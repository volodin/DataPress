using System.Web;
using System.Web.Optimization;

namespace DataPress.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/js/jquery").Include(
                        "~/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/js/jqueryui").Include(
                        "~/js/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/js/jqueryval").Include(
                        "~/js/jquery.unobtrusive*",
                        "~/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                        "~/js/modernizr-*"));

            bundles.Add(new StyleBundle("~/css").Include("~/css/site.css"));

            bundles.Add(new StyleBundle("~/css/themes/base/css").Include(
                        "~/css/themes/base/jquery.ui.core.css",
                        "~/css/themes/base/jquery.ui.resizable.css",
                        "~/css/themes/base/jquery.ui.selectable.css",
                        "~/css/themes/base/jquery.ui.accordion.css",
                        "~/css/themes/base/jquery.ui.autocomplete.css",
                        "~/css/themes/base/jquery.ui.button.css",
                        "~/css/themes/base/jquery.ui.dialog.css",
                        "~/css/themes/base/jquery.ui.slider.css",
                        "~/css/themes/base/jquery.ui.tabs.css",
                        "~/css/themes/base/jquery.ui.datepicker.css",
                        "~/css/themes/base/jquery.ui.progressbar.css",
                        "~/css/themes/base/jquery.ui.theme.css"));
        }
    }
}