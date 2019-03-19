using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;

namespace ZenithApp1.Controllers
{
    public class HomeController : Controller
    {
        ZenithContext context = new ZenithContext();

        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }            
        }
    }
}