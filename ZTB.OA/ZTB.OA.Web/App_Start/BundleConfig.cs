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
                        "~/Scripts/jquery.metisMenu.js",
                        "~/Scripts/jquery.slimscroll.min.js",
                        "~/Scripts/layer.min.js",
                        "~/Scripts/hplus.min.js",
                        "~/Scripts/pace.min.js",
                        "~/Scripts/contabs.min.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.cookie-1.4.1.min.js",
                        "~/Scripts/sweetalert.min.js"
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
                      "~/Scripts/bootstrap.min.js"
                     ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                         "~/Content/font-awesome.min.css",
                      "~/Content/animate.min.css",
                      "~/Content/style.min.css",
                      "~/Content/PagedList.css",
                      "~/Content/sweetalert.css",
                      "~/Content/iconfont.css"
                      ));
        }
    }
}
