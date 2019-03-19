using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.ViewModels;
using static ZenithApp1.Startup;

namespace ZenithApp1.Controllers
{
    public class SignupController : Controller
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
        [ValidateAntiForgeryToken]
        public ActionResult Index(SignUpViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationController ac = new AuthenticationController(HttpContext);

            var result = ac.AttemptSignUp(vm);

            switch (result)
            {
                case AuthenticationController.SignUpResult.UsernameExists:
                    ModelState.AddModelError("Username", "This username exists");
                    break;
                case AuthenticationController.SignUpResult.EmailExists:
                    ModelState.AddModelError("Email", "This email exists");
                    break;
                case AuthenticationController.SignUpResult.Success:
                    ac.SignIn(Models.User.getUserByEmail(vm.Email));
                    return RedirectToAction("Index", "Dashboard");
                case AuthenticationController.SignUpResult.Invalid:
                default:
                    ModelState.AddModelError(string.Empty, "Could not process your request. Please try again.");
                    return View(vm);
            }

            return View(vm);
        }
    }
}