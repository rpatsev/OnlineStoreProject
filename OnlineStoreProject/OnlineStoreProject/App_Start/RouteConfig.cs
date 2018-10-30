using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Protocols;

namespace OnlineStoreProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "CategoryProducts",
                url: "Product/ProductGroup/{id}",
                defaults: new { controller="Product", action="ProductsGroup", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineStoreProject.Controllers" }
            );

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional},
                namespaces: new[] { "OnlineStoreProject.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineStoreProject.Controllers" }
            );

            routes.MapRoute(
                "404-PageNotFound",
                "{*url}",
                new { controller = "StaticContent", action = "PageNotFound" },
                namespaces: new[] { "OnlineStoreProject.Controllers" }
            );
        }
    }
}
