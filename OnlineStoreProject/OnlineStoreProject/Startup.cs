using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using OnlineStoreProject.BLL.Interfaces;
using OnlineStoreProject.BLL.Services;
using OnlineStoreProject.DAL.EF;
using OnlineStoreProject.DAL.Entities;
using OnlineStoreProject.DAL.Identity;
using OnlineStoreProject.DAL.Interfaces;
using OnlineStoreProject.DAL.Repositories;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(OnlineStoreProject.Startup))]
namespace OnlineStoreProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            var hybridLifestyle = Lifestyle.CreateHybrid(defaultLifestyle: Lifestyle.Scoped, fallbackLifestyle: Lifestyle.Transient);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<DataContext>(Lifestyle.Scoped);
            container.Register(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            container.Register<IProductService, ProductService>();
            container.Register<IFeedbackService, FeedbackService>();
            container.Register<IAuthService, AuthService>(hybridLifestyle);
            container.Register<IUnitOfWork, IdentityUnitOfWork>(hybridLifestyle);
            container.Register<IClientManager, ClientManager>(hybridLifestyle);
            container.Register<IOrderService, OrderService>();
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }
    }
}
