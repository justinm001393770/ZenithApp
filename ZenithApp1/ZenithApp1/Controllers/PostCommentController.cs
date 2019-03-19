using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.ViewModels.Posts;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class PostCommentController : Controller
    {
        public ZenithContext db = new ZenithContext();
        public ActionResult Index()
        {
            //instantiate viewmodel object

            //add objects to viewmodel object

            //pass viewmodel object into view in next line
            return View();
        }

        [HttpPost]
        public ActionResult Create(PostsViewModel createPost)
        {
            //DateTime localDate = DateTime.Now;
           // DateTime utcDate = DateTime.UtcNow;
            PostComment newPost = new PostComment();
            //newPost.PostedByID = 6;
            newPost.PostID = createPost.comment.PostID;
            newPost.Comment = createPost.comment.Comment;
            newPost.IsActive = true;
            newPost.IsReported = false;
            //newPost.MediaID = 1;
            newPost.CreatedDate = DateTime.Now;
            newPost.UpdatedDate = DateTime.Now;
            newPost.PostedByID = LoggedInUser.UserID;
            db.PostComments.Add(newPost);
            db.SaveChanges();
            //string path = HttpContext.Current.Request.Url.AbsolutePath;
            //return View("Index");
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Update(PostCommentModel updatedPost)
        {
            PostComment result = (from p in db.PostComments
                                  where p.PostCommentID == updatedPost.PostCommentID
                                  select p).SingleOrDefault();

            result.Comment = updatedPost.Comment;

            db.SaveChanges();

            return View("Index");
        }

        [HttpPost]
        public ActionResult reportPost(PostCommentModel reportedPost)
        {
            PostComment result = (from p in db.PostComments
                                  where p.PostCommentID == reportedPost.PostCommentID
                                  select p).SingleOrDefault();

            result.IsReported = reportedPost.IsReported;

            db.SaveChanges();

            return View("Index");
        }

        [HttpPost]
        public ActionResult changePostActiveStatus(PostCommentModel statusPost)
        {
            PostComment result = (from p in db.PostComments
                                  where p.PostCommentID == statusPost.PostCommentID
                                  select p).SingleOrDefault();

            result.IsActive = statusPost.IsActive;

            db.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        public ActionResult adminUpdatePost(PostCommentModel updatedPost)
        {
            PostComment result = (from p in db.PostComments
                           where p.PostCommentID == updatedPost.PostCommentID
                                  select p).SingleOrDefault();

            result.Comment = updatedPost.Comment;
            result.IsActive = updatedPost.IsActive;
            result.IsReported = updatedPost.IsReported;
            //result.PostedByID = updatedPost.PostedByID;


            db.SaveChanges();
            return View("Index");
        }

        //Your Model
        public class PostCommentModel
        {

            [Required(ErrorMessage = "Comment is Required")]
            public string Comment { set; get; }

            public long PostedByID { get; set; }

            public long MediaID { get; set; }

            public long PostID { get; set; }

            public long PostCommentID { get; set; }

            public bool IsActive { get; set; }

            public bool IsReported { get; set; }

            public PostCommentModel() { }
        }
    }
}