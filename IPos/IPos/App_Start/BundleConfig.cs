using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace IPos.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Styles/AdminLTE/bootstrap/css/bootstrap.min.css",
                "~/Styles/Content/font-awesome.min.css",
                "~/Styles/Content/ionicons.min.css",
                "~/Styles/AdminLTE/dist/css/AdminLTE.min.css",
                "~/Styles/AdminLTE/dist/css/skins/_all-skins.min.css",
                "~/Styles/content/toastr.min.css"
            ));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Styles/AdminLTE/plugins/jQuery/jquery-2.2.3.min.js",
                "~/Styles/AdminLTE/bootstrap/js/bootstrap.min.js",
                "~/Styles/AdminLTE/plugins/slimScroll/jquery.slimscroll.min.js",
                "~/Styles/AdminLTE/plugins/fastclick/fastclick.min.js",
                "~/Styles/AdminLTE/dist/js/app.min.js",
                "~/Styles/Scripts/popper.min.js",
                "~/Styles/Scripts/toastr.min.js"
            ));
        }
    }
}