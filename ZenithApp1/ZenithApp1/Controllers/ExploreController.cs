using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.ViewModels.Explore;
using static ZenithApp1.Startup;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class ExploreController : Controller
    {
        public const int RecordsPerPage = 5;
        public List<Post> PostData;
        public List<Medium> MediaData;

        public ActionResult Index()
        {
            ZenithContext db = new ZenithContext();
            ExploreViewModel evm = new ExploreViewModel();
            IQueryable<Post> posts = Post.getPostsExplore(LoggedInUser.UserID);
            //IQueryable<Post> posts = DBMethods.getExplorePosts(LoggedInUser.UserID).AsQueryable();
            System.Web.HttpContext.Current.Session["ZenithExploreID"] = "0";

            PostData = posts.ToList();
            ViewBag.TotalNumberProjects = PostData.Count;
            ViewBag.Projects = GetRecordsForPage(0);

            evm.posts = posts.ToList();
            return View(evm);
        }

        public ActionResult Images()
        {
            ZenithContext db = new ZenithContext();
            ExploreViewModel evm = new ExploreViewModel();
            IQueryable<Medium> medias = Medium.getMediumExploreImages(LoggedInUser.UserID);
            //IQueryable<Post> posts = DBMethods.getExplorePosts(LoggedInUser.UserID).AsQueryable();
            System.Web.HttpContext.Current.Session["ZenithExploreID"] = "1";

            MediaData = medias.ToList();
            ViewBag.TotalNumberProjects = MediaData.Count;
            ViewBag.Projects = GetRecordsForPageM(0);

            evm.medias = medias.ToList();
            return View(evm);
        }

        public ActionResult Videos()
        {
            ZenithContext db = new ZenithContext();
            ExploreViewModel evm = new ExploreViewModel();
            IQueryable<Medium> medias = Medium.getMediumExploreVideos(LoggedInUser.UserID);
            //IQueryable<Post> posts = DBMethods.getExplorePosts(LoggedInUser.UserID).AsQueryable();
            System.Web.HttpContext.Current.Session["ZenithExploreID"] = "2";

            MediaData = medias.ToList();
            ViewBag.TotalNumberProjects = MediaData.Count;
            ViewBag.Projects = GetRecordsForPageM(0);

            evm.medias = medias.ToList();
            return View(evm);
        }

        public ActionResult Posts()
        {
            ZenithContext db = new ZenithContext();
            ExploreViewModel evm = new ExploreViewModel();
            IQueryable<Post> posts = Post.getPostsExplore(LoggedInUser.UserID);
            //IQueryable<Post> posts = DBMethods.getExplorePosts(LoggedInUser.UserID).AsQueryable();
            System.Web.HttpContext.Current.Session["ZenithExploreID"] = "3";

            PostData = posts.ToList();
            ViewBag.TotalNumberProjects = PostData.Count;
            ViewBag.Projects = GetRecordsForPage(0);

            evm.posts = posts.ToList();
            return View(evm);
        }

        [NoCache]
        public ActionResult GetProjects(int? pageNum)
        {
            pageNum = pageNum ?? 0;
            ViewBag.IsEndOfRecords = false;

            HttpContext context = System.Web.HttpContext.Current;
            int userId = Convert.ToInt32((string)(context.Session["ZenithPostUserID"]));
            int exploreId = Convert.ToInt32((string)(context.Session["ZenithExploreID"]));
            IQueryable<Medium> medias;

            if (Request.IsAjaxRequest())
            {
                if (exploreId == 0 || exploreId == 3)
                {
                    var projects = GetRecordsForPage(pageNum.Value);
                    ViewBag.IsEndOfRecords = (projects.Any());
                    return PartialView("_PostData", projects);
                }
                else if (exploreId == 1)
                {
                    var projectsI = GetRecordsForPageM(pageNum.Value);
                    ViewBag.IsEndOfRecords = (projectsI.Any());
                    return PartialView("_MediaData", projectsI);
                }
                else
                {
                    var projectsV = GetRecordsForPageM(pageNum.Value);
                    ViewBag.IsEndOfRecords = (projectsV.Any());
                    return PartialView("_MediaData", projectsV);
                }
            }
            else
            {

                ZenithContext db = new ZenithContext();
                if (exploreId == 0 || exploreId == 3)
                {
                    IQueryable<Post> posts = Post.getPostsExplore(LoggedInUser.UserID);

                    PostData = posts.ToList();

                    ViewBag.TotalNumberProjects = PostData.Count;
                    ViewBag.Projects = GetRecordsForPage(pageNum.Value);
                }
                else if (exploreId == 1)
                {
                    medias = Medium.getMediumExploreImages(LoggedInUser.UserID);

                    MediaData = medias.ToList();

                    ViewBag.TotalNumberProjects = MediaData.Count;
                    ViewBag.Projects = GetRecordsForPageM(pageNum.Value);
                }
                else
                {
                    medias = Medium.getMediumExploreImages(LoggedInUser.UserID);

                    MediaData = medias.ToList();

                    ViewBag.TotalNumberProjects = MediaData.Count;
                    ViewBag.Projects = GetRecordsForPageM(pageNum.Value);
                }

                return View("Index");
            }
        }

        [NoCache]
        public List<Post> GetRecordsForPage(int pageNum)
        {
            HttpContext context = System.Web.HttpContext.Current;
            int userId = Convert.ToInt32((string)(context.Session["ZenithPostUserID"]));
            int exploreId = Convert.ToInt32((string)(context.Session["ZenithExploreID"]));

            ZenithContext db = new ZenithContext();
                IQueryable<Post> posts = Post.getPostsExplore(LoggedInUser.UserID);

                PostData = posts.ToList();

                int from = (pageNum * RecordsPerPage);

                var tempList = (from rec in PostData
                                select rec).Skip(from).Take(4).ToList();

                return tempList;
        }

        [NoCache]
        public List<Medium> GetRecordsForPageM(int pageNum)
        {
            HttpContext context = System.Web.HttpContext.Current;
            int userId = Convert.ToInt32((string)(context.Session["ZenithPostUserID"]));
            int exploreId = Convert.ToInt32((string)(context.Session["ZenithExploreID"]));

            IQueryable<Medium> medias;

            ZenithContext db = new ZenithContext();
            if (exploreId == 1)
            {
                medias = Medium.getMediumExploreImages(LoggedInUser.UserID);
            } else
            {
                medias = Medium.getMediumExploreVideos(LoggedInUser.UserID);
            }

            MediaData = medias.ToList();

            int from = (pageNum * RecordsPerPage);

            var tempList = (from rec in MediaData
                            select rec).Skip(from).Take(4).ToList();

            return tempList;
        }
    }
}