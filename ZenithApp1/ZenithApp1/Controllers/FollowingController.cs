using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class FollowingController : Controller
    {
        public ActionResult Index()
        {
            User user = Models.User.getUserById(LoggedInUser.UserID);
            List<Following> following = user.Followings.ToList();
            return View(following);
        }

        public ActionResult OtherFollowing(string userName)
        {
            User user = Models.User.getUserByUsername(userName);
            if (user == null || user.UserID == LoggedInUser.UserID)
            {
                return RedirectToAction("Index");
            }
            List<Following> following = user.Followings.ToList();
            return View(following);
        }
    }
}