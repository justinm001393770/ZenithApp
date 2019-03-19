using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models
{
    public static class LoggedInUser
    {
        public static int UserID
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["UserId"]); }
        }

        public static string UserName
        {
            get { return HttpContext.Current.Session["UserUsername"].ToString(); }
        }

        public static string UserProfileImage
        {
            get { return HttpContext.Current.Session["UserProfileImage"].ToString(); }
        }

        public static int UserFollowerCount
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["UserFollowerCount"]); }
        }

        public static int UserFollowingCount
        {
            get { return Convert.ToInt32(HttpContext.Current.Session["UserFollowingCount"]); }
        }

        public static bool IsFollowingThisId(long id)
        {
            User followeeUser = User.getUserById(id);

            int count = followeeUser.Followings1.Where(x => x.FollowerID == UserID).Count();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void setSessionVariables(User user)
        {
            try
            {
                HttpContext.Current.Session["UserId"] = user.UserID.ToString();
                HttpContext.Current.Session["UserProfileImage"] = user.getProfilePicture();
                HttpContext.Current.Session["UserUsername"] = user.UserName.ToString();
                HttpContext.Current.Session["UserFollowerCount"] = user.Followings1.Count().ToString();
                HttpContext.Current.Session["UserFollowingCount"] = user.Followings.Count().ToString();
            }
            catch (Exception ex)
            {

            }
           
        }

        public static bool checkIfHasGameInLibrary(long gameID)
        {
            ZenithContext db = new ZenithContext();
            GameLibrary gl = db.GameLibraries.Where(a => a.UserID == LoggedInUser.UserID && a.GameID == gameID).SingleOrDefault();
            if(gl != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}