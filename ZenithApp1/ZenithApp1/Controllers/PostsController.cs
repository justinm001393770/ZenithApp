using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Collections;
using ZenithApp1.Models;
using static ZenithApp1.Startup;


namespace ZenithApp1.Controllers
{
    public class PostsController : Controller
    {
        public const int RecordsPerPage = 5;
        public List<PostComment> PostCommentData;

        public PostsController()
        {
            ViewBag.RecordsPerPage = RecordsPerPage;
        }
        

        // GET: Game
        [Route("Posts/{?postId}")]
        public ActionResult Index(String pId)
        {
            int postId = Convert.ToInt32(RouteData.Values["postId"]);
            System.Web.HttpContext.Current.Session["ZenithPostID"] = postId.ToString();
            //IQueryable<Post> posts = db.Posts.Take(800);
            IQueryable<PostComment> postComments = PostComment.getPostComments(postId);
            //IQueryable<Post> posts = Post.getPostsFollowedI(6);

            PostCommentData = postComments.ToList();
            ViewBag.TotalNumberProjects = PostCommentData.Count;
            ViewBag.Projects = GetRecordsForPage(0);

            var post = new Post();
            ViewModels.Posts.PostsViewModel pvm = new ViewModels.Posts.PostsViewModel();
            pvm.id = postId;
            pvm.post = Post.getPostById(postId);
            pvm.thisUser = Models.User.getUserById(LoggedInUser.UserID);
            pvm.usersMedia = Models.User.getMedia(LoggedInUser.UserID);

            return View(pvm);
        }

        // GET: Game
        [Route("Posts/Edit/{?postId}")]
        public ActionResult Edit(String pId)
        {
            int postId = Convert.ToInt32(RouteData.Values["postId"]);
            System.Web.HttpContext.Current.Session["ZenithPostID"] = postId.ToString();

            var post = new Post();
            ViewModels.Posts.PostsViewModel pvm = new ViewModels.Posts.PostsViewModel();
            pvm.id = postId;
            pvm.post = Post.getPostById(postId);
            pvm.thisUser = Models.User.getUserById(LoggedInUser.UserID);
            pvm.usersMedia = Models.User.getMedia(LoggedInUser.UserID);

            if (LoggedInUser.UserID == pvm.post.PostedByID)
            {
                return View(pvm);
            }
            else
            {
                return Redirect("~/Posts/" + postId.ToString());
            }
        }


        [NoCache]
        public ActionResult GetProjects(int? pageNum)
        {
            HttpContext context = System.Web.HttpContext.Current;
            int postId = Convert.ToInt32((string)(context.Session["ZenithPostID"]));
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;
            if (Request.IsAjaxRequest())
            {
                var projects = GetRecordsForPage(pageNum.Value);
                ViewBag.IsEndOfRecords = (projects.Any());
                return PartialView("_PostCommentData", projects);
            }
            else
            {

                ZenithContext db = new ZenithContext();
                //IQueryable<Post> posts = db.Posts.Take(800);
                IQueryable<PostComment> postComments = PostComment.getPostComments(postId);
                //IQueryable<Post> posts = Post.getPostsFollowedI(6);

                PostCommentData = postComments.ToList();

                ViewBag.TotalNumberProjects = PostCommentData.Count;
                ViewBag.Projects = GetRecordsForPage(pageNum.Value);

                return View("Index");
            }
        }

        [NoCache]
        public List<PostComment> GetRecordsForPage(int pageNum)
        {
            ZenithContext db = new ZenithContext();
            HttpContext context = System.Web.HttpContext.Current;
            int postId = Convert.ToInt32((string)(context.Session["ZenithPostID"]));
            //IQueryable<Post> posts = db.Posts.Take(800);
            IQueryable<PostComment> postComments = PostComment.getPostComments(postId);
            //IQueryable<Post> posts = Post.getPostsFollowedI(6);

            PostCommentData = postComments.ToList();

            int from = (pageNum * RecordsPerPage);

            var tempList = (from rec in PostCommentData
                            select rec).Skip(from).Take(5).ToList();

            return tempList;
        }

    }
}