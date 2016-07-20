using System.Web;
using System.Web.Optimization;

namespace SportsRUsApp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/font-awesome.css",
                      "~/Content/css/revolution-slider.css",
                      "~/Content/css/owl.css",
                      "~/Content/css/responsive.css",
                      "~/Content/css/main.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/Scripts/bootstrap.js",
                      "~/Content/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Content/Scripts/jquery-ui-1.11.4.js",
                        "~/Content/js/jquery.fancybox.pack.js",
                        "~/Content/js/revolution.min.js",
                        "~/Content/js/owl.carousel.min.js",
                        "~/Content/js/bxslider.js",
                        "~/Content/js/wow.js",
                        "~/Content/js/script.js",
                        "~/Content/js/custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/appScripts").Include(
                        "~/Scripts/notifications.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

        }
    }
}
