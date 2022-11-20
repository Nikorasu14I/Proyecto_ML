using System.Web;
using System.Web.Optimization;

namespace CapaPresentacion
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/complementos").Include(             
                        "~/Scripts/scripts.js",
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new Bundle("~/bundles/bootstra").Include(
                "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/csslogin").Include(
                    "~/Content/css/Login.css"));

            bundles.Add(new StyleBundle("~/Content/csslogin1").Include(
                    "~/Content/css/Login.css"));

            //Datable
            bundles.Add(new StyleBundle("~/Content/Data/css").Include(
                      "~/Content/Datatable/css/jquery.dataTables.min.css",
                      "~/Content/Datatable/css/buttons.dataTables.min.css",
                        "~/Content/fontawesome-free/css/all.css",
                         "~/Content/jquery-ui.css"

                      ));

            bundles.Add(new StyleBundle("~/Content/Data/js").Include(
                      "~/Content/Datatable/js/jquery.dataTables.min.js",
                      "~/Content/Datatable/js/dataTables.buttons.min.js",          
                         "~/Scripts/fontawesome-free/all.js"
                      ));

        }
    }
}
