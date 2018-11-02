using System.Web;
using System.Web.Optimization;
using System.Web.UI.WebControls;

namespace OnlineStoreProject
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.12.1.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                    "~/Scripts/grid.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                    "~/Scripts/script.js",
                    "~/Scripts/sidebar.js"));

            bundles.Add(new ScriptBundle("~/bundles/feedbacks").Include(
                    "~/Scripts/feedbacks.js"));

            bundles.Add(new ScriptBundle("~/bundles/rating").Include(
                    "~/Scripts/jquery.rating-2.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/vote").Include(
                    "~/Scripts/vote.js"));

            bundles.Add(new ScriptBundle("~/bundles/cart").Include(
                    "~/Scripts/cart.js"));

            bundles.Add(new ScriptBundle("~/bundles/orders").Include(
                    "~/Scripts/admin/orders.js"));

            bundles.Add(new StyleBundle("~/Content/jqueryuistyle").Include(
                    "~/Content/themes/base/jquery-ui.min.css",
                    "~/Content/themes/base/all.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/order.css",
                      "~/Content/privatecabinet.css",
                      "~/Content/homepage.css",
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/categories.css",
                      "~/Content/products.css",
                      "~/Content/feedbacks.css",
                      "~/Content/jquery.rating.css",
                      "~/Content/slick/slick.css",
                      "~/Content/slick/slick-theme.css"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                "~/Content/admin.css"));
        }
    }
}
