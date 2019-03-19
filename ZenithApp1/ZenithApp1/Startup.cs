using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Net;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

[assembly: OwinStartup(typeof(ZenithApp1.Startup))]

namespace ZenithApp1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Login/"),
                Provider = new CookieAuthenticationProvider { OnApplyRedirect = CustomRedirect },
                ExpireTimeSpan = TimeSpan.FromDays(5),
                SlidingExpiration = true
            });

            app.MapSignalR();
        }

        private static void CustomRedirect(CookieApplyRedirectContext context)
        {
            //var redirectUrl = context.Options.LoginPath.ToString();
            //if (context.Request.Method == WebRequestMethods.Http.Get)
            //{
            //    var returnUrl = context.Request.Path.ToString();
            //    if (!string.IsNullOrEmpty(returnUrl) && !returnUrl.Equals("/"))
            //        redirectUrl += "?" + context.Options.ReturnUrlParameter + "=" + returnUrl;
            //}
            //else if (context.Request.Method == WebRequestMethods.Http.Post)
            //{
            //    //TODO: add toastr message showing that the post did not succeed
            //}
            context.Response.Redirect("/Login");
        }

        public class NoCacheAttribute : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                filterContext.HttpContext.Response.Cache.SetNoStore();

                base.OnResultExecuting(filterContext);
            }
        }

        public class VerifiedAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuted(ActionExecutedContext filterContext)
            {
                
                filterContext.Result = new RedirectToRouteResult(
                           new RouteValueDictionary
                           {
                                       { "action", "Index" },
                                       { "controller", "Home" }
                           });
                base.OnActionExecuted(filterContext);

            }
        }

        public class UserFilterAttribute : FilterAttribute, IActionFilter
        {
            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
            }

            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var username = filterContext.RouteData.Values["userName"];

                if(username == null)
                {
                
                }
                else
                {
                    string usernameString = username.ToString();

                    if (!Models.User.usernameExists(usernameString))
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Profile", action = "Index", userName = Models.LoggedInUser.UserName }));
                    }
                }
            }
        }
    }
}
