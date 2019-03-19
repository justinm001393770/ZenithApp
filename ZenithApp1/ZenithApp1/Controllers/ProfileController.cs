using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.ViewModels.Profile;
using static ZenithApp1.Startup;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public const int RecordsPerPage = 5;
        public List<Post> PostData;
        public ProfileController()
        {
            ViewBag.RecordsPerPage = RecordsPerPage;
        }

        [HttpGet]
        [UserFilter]
        public ActionResult Index(string username)
        {
            RouteData.Values.Remove("userName");

            ProfileViewModel vm = new ProfileViewModel();
            ZenithContext db = new ZenithContext();
            var userProfile = new User();
            if (username == null)
            {
                userProfile = Models.User.getUserByUsername(LoggedInUser.UserName);
            }
            else
            {
                userProfile = Models.User.getUserByUsername(username);
            }
            System.Web.HttpContext.Current.Session["ZenithPostUserID"] = userProfile.UserID.ToString();

            //IQueryable<Post> posts = db.Posts.Take(800);
            IQueryable<Post> posts = Post.getPostsFollowedI(userProfile.UserID);
            //IQueryable<Post> posts = Post.getPostsFollowedI(6);

            PostData = posts.ToList();
            ViewBag.TotalNumberProjects = PostData.Count;
            ViewBag.Projects = GetRecordsForPage(0);

            vm.posts = posts.ToList();
            vm.profileOwner = Models.User.getUserByUsername(username);
            vm.usersMedia = Models.User.getMediaTop5(LoggedInUser.UserID);
            if (vm.profileOwner == null || vm.profileOwner.UserName == LoggedInUser.UserName)
            {
                if(username == LoggedInUser.UserName)
                {
                    return RedirectToAction("Index", "Profile");
                }

                vm.profileOwner = Models.User.getUserById(LoggedInUser.UserID);
                return View(vm);
            }

            vm.profileOwner = Models.User.getUserByUsername(username);

            return View(vm);
        }
        public ActionResult Follow(string username)
        {
            //ProfileViewModel vm = new ProfileViewModel();
            User user = Models.User.getUserByUsername(username);
            if(Models.Following.addFollower(user.UserID) == true)
            {
                return RedirectToAction("Index", "Profile", new { userName = username });
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        public ActionResult Unfollow(string username)
        {
            User user = Models.User.getUserByUsername(username);
            if (Models.Following.removeFollower(user.UserID) == true)
            {
                return RedirectToAction("Index", "Profile", new { userName = username });
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        public ActionResult Unfollowee(string username)
        {
            User user = Models.User.getUserByUsername(username);
            if (Models.Following.removeFollowee(user.UserID) == true)
            {
                return RedirectToAction("Index", "Profile", new { userName = username });
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
        }

        [HttpGet]
        public ActionResult Edit()
        {
            User user = Models.User.getUserById(LoggedInUser.UserID);
            EditViewModel evm = new EditViewModel();
            evm.Email = user.Email;
            evm.FirstName = user.FirstName;
            evm.LastName = user.LastName;
            evm.Phone = user.Phone;
            return View(evm);
        }

        [HttpPost]
        public ActionResult Edit(EditViewModel user)
        {
            if(user.Password != user.confirmPassword)
            {
                return View(user);
            }
            ZenithContext db = new ZenithContext();
            using (var transaction = db.Database.BeginTransaction())
            {

                User currentUser = (from u in db.Users
                                    where u.UserID == LoggedInUser.UserID
                                    select u).SingleOrDefault();
                if(user.Email != null)
                {
                    currentUser.Email = user.Email;
                }
                if(user.FirstName != null)
                {
                    currentUser.FirstName = user.FirstName;
                }
                if (user.LastName != null)
                {
                    currentUser.LastName = user.LastName;
                }
                if (user.Phone != null)
                {
                    currentUser.Phone = user.Phone;
                }

                if(user.Password != "" && user.Password != null)
                {
                    Salt salt = (from s in db.Salts
                                 where s.UserID == LoggedInUser.UserID
                                 select s).SingleOrDefault();
                    currentUser.Password = AuthenticationController.CreatePasswordHash(salt.SaltValue, user.Password);
                }
                
                if(user.ProfilePic != "" && user.ProfilePic != null)
                {
                    
                    Medium media = new Medium();
                    media.UserID = LoggedInUser.UserID;
                    string mediaName = LoggedInUser.UserName + "ProfilePicture";
                    media.MediaName = mediaName;
                    media.MediaPath = user.ProfilePic;
                    media.IsPublic = true;
                    media.CreatedDate = DateTime.Now;
                    db.Media.Add(media);

                    db.SaveChanges();

                    //Incoming jank
                    /*List<Medium> media2 = db.Media.Where(m => m.UserID == LoggedInUser.UserID)
                        .Where(m => m.MediaName == media.MediaName)
                        .Where(m => m.MediaPath == media.MediaPath)
                        .Where(m => m.CreatedDate == media.CreatedDate).ToList();*/

                    currentUser.ProfilePicMediaID = media.MediaID;
                    
                }
                if(user.file.ToList()[0] != null)
                {
                    S3Controller s3 = new S3Controller();
                    s3.UploadFiles(user.file, LoggedInUser.UserName);
                    string s3url = "";
                    foreach(HttpPostedFileBase files in user.file)
                    {
                        s3url = "https://s3.amazonaws.com/zenith-user-images/" + LoggedInUser.UserName + "/" + files.FileName;
                    }
                    Medium media = new Medium();
                    media.UserID = LoggedInUser.UserID;
                    media.MediaName = LoggedInUser.UserName + "ProfilePicture";
                    media.MediaPath = s3url;
                    media.IsPublic = true;
                    media.CreatedDate = DateTime.Now;
                    db.Media.Add(media);

                    db.SaveChanges();

                    currentUser.ProfilePicMediaID = media.MediaID;
                }
                currentUser.Active = true;

                //db.Entry(currentUser).CurrentValues.SetValues(user);
                db.SaveChanges();
                transaction.Commit();
            }
            return Redirect("Dashboard");
        }

        [NoCache]
        public ActionResult GetProjects(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;

            HttpContext context = System.Web.HttpContext.Current;
            int userId = Convert.ToInt32((string)(context.Session["ZenithPostUserID"]));

            if (Request.IsAjaxRequest())
            {
                var projects = GetRecordsForPage(pageNum.Value);
                ViewBag.IsEndOfRecords = (projects.Any());
                return PartialView("_PostData", projects);
            }
            else
            {

                ZenithContext db = new ZenithContext();
                //IQueryable<Post> posts = db.Posts.Take(800);
                IQueryable<Post> posts = Post.getPostsFollowedI(userId);
                //IQueryable<Post> posts = Post.getPostsFollowedI(6);

                PostData = posts.ToList();

                ViewBag.TotalNumberProjects = PostData.Count;
                ViewBag.Projects = GetRecordsForPage(pageNum.Value);

                return View("Index");
            }
        }

        [NoCache]
        public List<Post> GetRecordsForPage(int pageNum)
        {
            HttpContext context = System.Web.HttpContext.Current;
            int userId = Convert.ToInt32((string)(context.Session["ZenithPostUserID"]));

            ZenithContext db = new ZenithContext();
            //IQueryable<Post> posts = db.Posts.Take(800);
            IQueryable<Post> posts = Post.getPostsFollowedI(userId);
            //IQueryable<Post> posts = Post.getPostsFollowedI(6);

            PostData = posts.ToList();

            int from = (pageNum * RecordsPerPage);

            var tempList = (from rec in PostData
                            select rec).Skip(from).Take(5).ToList();

            return tempList;
        }
    }
}