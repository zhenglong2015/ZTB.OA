using System.Web;
using System.Web.Optimization;

namespace ZTB.OA.Web
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Content/js/jquery.metisMenu.js",
                        "~/Content/js/jquery.slimscroll.min.js",
                        "~/Content/js/layer.min.js",
                        "~/Content/js/hplus.min.js",
                        "~/Content/js/pace.min.js",
                        "~/Content/js/contabs.min.js",
                        "~/Content/js/pace.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.cookie-1.4.1.min.js",
                        "~/Content/dist/sweetalert-dev.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Content/js/bootstrap.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/bootstrap.min.css",
                         "~/Content/css/font-awesome.min.css",
                      "~/Content/css/animate.min.css",
                      "~/Content/css/style.min.css",
                      "~/Content/PagedList.css",
                      "~/Content/dist/sweetalert.css"
                      ));
        }
    }
}
