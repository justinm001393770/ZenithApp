using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class FollowersController : Controller
    {
        public ActionResult Index()
        {
            User user = Models.User.getUserById(LoggedInUser.UserID);
            List<Following> following = user.Followings1.ToList();
            return View(following);
        }

        public ActionResult OtherFollowers(string userName)
        {
            User user = Models.User.getUserByUsername(userName);
            if (user == null || user.UserID == LoggedInUser.UserID)
            {
                return RedirectToAction("Index");
            }
            User toFollow = Models.User.getUserByUsername(userName);
            List<Following> following = user.Followings1.ToList();
            return View(following);
        }
    }
}