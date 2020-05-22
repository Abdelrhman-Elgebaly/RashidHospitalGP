using System.Web;
using System.Web.Optimization;

namespace RashidHospital
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/jquery-3.4.1.min.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/adminlte.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/adminlte").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/font-awesome.min.css",
                "~/Content/AdminLTE.min.css",
                "~/Content/skin-purple.min.css",
                "~/Content/taskedInWeb.css"));
        }
    }
}
