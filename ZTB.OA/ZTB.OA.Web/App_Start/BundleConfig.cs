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
                        "~/Scripts/sweetalert.min.js",
                        "~/Content/imgareaselect/scripts/jquery.imgareaselect.pack.js",
                        "~/Scripts/jquery.form.min.js",
                        "~/Content/cropper/cropper.min.js",
                        "~/Scripts/bootstrap-paginator.js",
                        "~/Scripts/toastr.min.js",
                        "~/Content/switchery/switchery.min.js",
                        "~/Scripts/jquery.validate.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                     "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
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
                      "~/Content/iconfont.css",
                      "~/Content/imgareaselect/css/imgareaselect-default.css",
                      "~/Content/cropper/cropper.min.css",
                      "~/Content/toastr.min.css",
                      "~/Content/switchery/switchery.min.css"
                      ));
        }
    }
}
