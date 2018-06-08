using System.Web.Optimization;

namespace WebAppEC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                    "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/autocomplete").Include(
                  "~/Scripts/autocomplete.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/app.css",
                      "~/Content/css/login.css",
                      "~/Content/css/cadastrar.css",
                      "~/Content/css/pagamento.css",
                      "~/Content/css/detalheProduto.css",
                      "~/Content/css/jquery-ui.css",
                      "~/Content/css/jquery-ui.theme.css",
                      "~/Content/css/listCcategoria.css",
                      "~/Content/css/list-index.css"
                ));

        }
    }
}
