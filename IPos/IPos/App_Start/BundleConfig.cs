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
                "~/Styles/AdminLTE/bower_components/bootstrap/dist/css/bootstrap.min.css",
                "~/Styles/AdminLTE/bower_components/font-awesome/css/font-awesome.min.css",
                "~/Styles/AdminLTE/bower_components/Ionicons/css/ionicons.min.css",
                "~/Styles/AdminLTE/dist/css/AdminLTE.min.css",
                "~/Styles/AdminLTE/dist/css/skins/_all-skins.min.css"
            ));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Styles/AdminLTE/bower_components/jquery/dist/jquery.min.js",
                "~/Styles/AdminLTE/bower_components/bootstrap/dist/js/bootstrap.min.js",
                "~/Styles/AdminLTE/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                "~/Styles/AdminLTE/bower_components/fastclick/lib/fastclick.js",
                "~/Styles/AdminLTE/dist/js/adminlte.min.js",
                "~/Styles/Scripts/popper.min.js"
            ));
        }
    }
}