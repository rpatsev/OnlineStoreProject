using Microsoft.AspNet.Identity;
using OnlineStoreProject.DAL.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreProject.DAL.Identity
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        //public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store) { }
        private IUserStore<ApplicationUser> store;
        public ApplicationUserManager(IUserStore<ApplicationUser> _store) : base(_store)
        {
            this.store = _store;
        }
    }
}
