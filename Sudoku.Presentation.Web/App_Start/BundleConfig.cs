using System.Web.Optimization;

namespace Sudoku.Presentation.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/css")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/master.min.css")
                );

            bundles.Add(new ScriptBundle("~/bundles/scripts")
                .Include("~/Scripts/bootstrap.min.js")
                .Include("~/Scripts/knockout-2.2.1.min.js")
                );
        }
    }
}