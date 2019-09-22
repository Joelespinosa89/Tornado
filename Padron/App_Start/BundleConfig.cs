﻿using System.Web;
using System.Web.Optimization;

namespace Padron
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/jquery.easing.min.js",
                       "~/Scripts/jquery.dataTables.js",
                      "~/Scripts/dataTables.bootstrap4.js",
                      "~/Scripts/sb-admin.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                    "~/Content/sb-admin/bootstrap.min.css",
                    "~/Content/font/css/all.min.css",
                      "~/Content/dataTables.bootstrap4.css",
                      "~/Content/sb-admin.css",
                      "~/Content/site.css"));
        }
    }
}
