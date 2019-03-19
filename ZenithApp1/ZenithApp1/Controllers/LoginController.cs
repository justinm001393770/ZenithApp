using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.ViewModels;

namespace ZenithApp1.Controllers
{
    public class LoginController : Controller
    {
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
        
        [HttpPost]
        public ActionResult Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationController ac = new AuthenticationController(HttpContext);

            var result = ac.AttemptLogin(vm.Email, vm.Password);

            switch (result)
            {
                case AuthenticationController.LoginResult.Success:
                    ac.SignIn(Models.User.getUserByEmail(vm.Email));
                    return RedirectToAction("Index", "Dashboard");
                case AuthenticationController.LoginResult.RequireVerification:
                    ac.SignIn(Models.User.getUserByEmail(vm.Email));
                    return RedirectToAction("Index", "Dashboard");
                case AuthenticationController.LoginResult.Failure:
                    ModelState.AddModelError(string.Empty, "Login failed, please try again.");
                    break;
                default:
                    ModelState.AddModelError(string.Empty, "Could not process your request. Please try again.");
                    return View(vm);
            }
            return View(vm);
        }
    }
}