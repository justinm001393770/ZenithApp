using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ZenithApp1.Models;

namespace ZenithApp1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Session_Start(object sender, EventArgs e)
        {
            if (Session["UserId"] == null || Session["UserId"].ToString() == "")
            {
                if(User.Identity.Name != null && User.Identity.Name.ToString() != "")
                {
                    User user = Models.User.getUserById(Convert.ToInt32(User.Identity.Name));

                    Models.LoggedInUser.setSessionVariables(user);
                }
            }
        }

        internal protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (User != null && User.Identity.IsAuthenticated == true)
            {
                int id = Convert.ToInt32(User.Identity.Name);
                Models.LoggedInUser.setSessionVariables(Models.User.getUserById(id));
            }
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }


    }
}
