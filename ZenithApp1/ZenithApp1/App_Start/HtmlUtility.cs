using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZenithApp1
{
    public static class HtmlUtility
    {
        public static string IsActive(this HtmlHelper html, string controller)
        {
            var routeData = html.ViewContext.RouteData;

          
            var routeController = (string)routeData.Values["controller"];

            var returnActive = controller == routeController;

            return returnActive ? "active sidebar-btn" : "sidebar-btn";
        }

        public static string IsActive(this HtmlHelper html, string action, string controller)
        {
            var routeData = html.ViewContext.RouteData;

            var routeAction = (string)routeData.Values["action"];
            var routeController = (string)routeData.Values["controller"];

            var returnActive = controller == routeController && action == routeAction;

            return returnActive ? "active sidebar-btn" : "sidebar-btn";
        }
    }
}