using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.ViewModels.Dashboard;
using static ZenithApp1.Startup;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        ZenithContext context = new ZenithContext();
        S3Controller s3 = new S3Controller();

        public const int RecordsPerPage = 5;
        public List<Post> PostData;
        public DashboardController()
        {
            ViewBag.RecordsPerPage = RecordsPerPage;
        }

        public ActionResult Index()
        {
            DashboardViewModel vm = new DashboardViewModel();
            //IQueryable<Post> posts = db.Posts.Take(800);
            IQueryable<Post> posts = Post.getPostsDash(LoggedInUser.UserID);
            //IQueryable<Post> posts = Post.getPostsFollowedI(6);

            PostData = posts.ToList();
            ViewBag.TotalNumberProjects = PostData.Count;
            ViewBag.Projects = GetRecordsForPage(0);

            vm.usersMedia = Models.User.getMediaTop5(LoggedInUser.UserID);

            vm.posts = posts.ToList();

            return View(vm);
        }

        [HttpPost]
        public ActionResult UploadFiles(IEnumerable<HttpPostedFileBase> files)
        {
            bool success = s3.UploadFiles(files, Session["UserUsername"].ToString());

            if (success) {
                return Json("file uploaded successfully");
            }
            else
            {
                return Json("file upload failed");
            }
        }

        [NoCache]
        public ActionResult GetProjects(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;

            HttpContext context = System.Web.HttpContext.Current;
            int userId = Convert.ToInt32(LoggedInUser.UserID);

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
                IQueryable<Post> posts = Post.getPostsDash(userId);
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
            int userId = Convert.ToInt32(LoggedInUser.UserID);

            ZenithContext db = new ZenithContext();
            //IQueryable<Post> posts = db.Posts.Take(800);
            IQueryable<Post> posts = Post.getPostsDash(userId);
            //IQueryable<Post> posts = Post.getPostsFollowedI(6);

            PostData = posts.ToList();

            int from = (pageNum * RecordsPerPage);

            var tempList = (from rec in PostData
                            select rec).Skip(from).Take(5).ToList();

            return tempList;
        }
    }
}