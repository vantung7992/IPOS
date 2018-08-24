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
                "~/Styles/Content/bootstrap.min.css",
                "~/Styles/Content/font-awesome.min.css",
                "~/Styles/admin-lte/css/skins/_all-skins.css",
                "~/Styles/admin-lte/css/AdminLTE.min.css",
                "~/Styles/Content/Site.css"
            ));

            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Scripts/modernizr-2.6.2.js",
                "~/Styles/Scripts/jquery-3.1.1.min.js",
                "~/Scripts/bootstrap.min.js"
            ));
        }
    }
}