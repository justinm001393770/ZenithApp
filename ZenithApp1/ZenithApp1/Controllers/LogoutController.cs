using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ZenithApp1.Startup;

namespace ZenithApp1.Controllers
{
    [NoCache]
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            AuthenticationController ac = new AuthenticationController(HttpContext);
            ac.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}