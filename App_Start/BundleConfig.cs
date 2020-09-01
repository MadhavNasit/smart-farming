using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SmartFarming.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/bundles/jquery")
               .Include("~/Scripts/jquery-3.5.1.min.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/bootstrap.bundle.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/scripts")
               .Include("~/Scripts/swiper.min.js",
                        "~/Scripts/autoscroll.js",
                        "~/Scripts/owl.carousel.js",
                        "~/Scripts/jquery-ui.min.js"
                        ));

            bundles.Add(new StyleBundle("~/bundles/admincss")
               .Include("~/Css/Admin/bootstrap.min.css",
                        "~/Css/Tabs&Pills.css",
                        "~/Css/Admin/style.css"
                        ));

            bundles.Add(new StyleBundle("~/bundles/css")
               .Include("~/Css/font-awesome.min.css",
                        "~/Css/owl.carousel.css",
                        "~/Css/swiper.css",
                        "~/Content/bootstrap.min.css",
                        "~/Css/style.css"
                        ));

            bundles.Add(new StyleBundle("~/bundles/logincss")
               .Include("~/Fonts/material-icon/css/material-design-iconic-font.css",
                        "~/Content/bootstrap.css",
                        "~/Css/LogIn/style.css"
                        ));

            BundleTable.EnableOptimizations = true;
        }
    }
}