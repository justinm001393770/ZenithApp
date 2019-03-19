using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.ViewModels.Dashboard;
using ZenithApp1.ViewModels.Posts;
using ZenithApp1.ViewModels.Profile;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class PostController : Controller
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
        public ActionResult Create(DashboardViewModel createPost)
        {
            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;
            Post newPost = new Post();
            newPost.PostedByID = createPost.post.PostedByID;
            newPost.PostedToID = createPost.post.PostedToID;
            newPost.PostTitle = createPost.post.PostTitle;
            newPost.PostContents = createPost.post.PostContents;
            newPost.IsActive = createPost.post.IsActive;
            newPost.IsReported = createPost.post.IsReported;
            newPost.CreatedDate = DateTime.Now;
            newPost.UpdatedDate = DateTime.Now;
            if (String.IsNullOrWhiteSpace(createPost.post.PostTitle))
                newPost.PostTitle = "";
            if (String.IsNullOrWhiteSpace(createPost.post.PostContents))
                ModelState.AddModelError("PostContents", "PostContents is required.");
            if (!ModelState.IsValid)
                return Redirect(Request.UrlReferrer.ToString());
            // return Redirect(Request.UrlReferrer.ToString());
            /*if (newPost.PostedByID == 0)
                newPost.PostedByID = 6;
            if (newPost.PostedToID == 0)
                newPost.PostedToID = 6;*/
            if (createPost.editPostMedias != null)
            { 
                foreach (string m in createPost.editPostMedias)
                {
                    PostMedia newMedium = new PostMedia();
                    newMedium.MediaID = Convert.ToInt32(m);
                    newPost.PostMedias.Add(newMedium);
                }
            }

            db.Posts.Add(newPost);
            db.SaveChanges();

            //return View("Index");
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Update(PostsViewModel updatedPost)
        {
            DateTime localDate = DateTime.Now;
            DateTime utcDate = DateTime.UtcNow;
            Post result = (from p in db.Posts
                           where p.PostID == updatedPost.post.PostID
                           select p).SingleOrDefault();
            result.PostTitle = updatedPost.post.PostTitle;
            result.PostContents = updatedPost.post.PostContents;
            result.UpdatedDate = DateTime.Now;
            if (String.IsNullOrWhiteSpace(updatedPost.post.PostContents))
                ModelState.AddModelError("PostContents", "PostContents is required.");
            // return Redirect(Request.UrlReferrer.ToString());

            if (String.IsNullOrWhiteSpace(updatedPost.post.PostTitle))
                result.PostTitle = "";
            if (!ModelState.IsValid)
                return Redirect(Request.UrlReferrer.ToString());

            foreach (PostMedia m in result.PostMedias.ToList())
            {
                if (updatedPost.editPostMedias != null)
                {
                    if (!updatedPost.editPostMedias.ToList().Exists(x => x == m.MediaID.ToString()))
                    {
                        db.PostMedias.Remove(m);
                    }
                }
                else
                {
                    result.PostMedias.Remove(m);
                }
                
            }
            

            if (updatedPost.editPostMedias != null)
            {
                foreach (string m in updatedPost.editPostMedias)
                {
                    if (!result.PostMedias.ToList().Exists(x => x.MediaID.ToString() == m))
                        {
                        PostMedia newMedium = new PostMedia();
                        newMedium.MediaID = Convert.ToInt32(m);
                        result.PostMedias.Add(newMedium);
                    }
                }
            }

            db.SaveChanges();
            
            //return View("Index");
            return Redirect("~/Posts/"+ updatedPost.post.PostID.ToString());
        }

        [HttpPost]
        public ActionResult Report(PostsViewModel reportPost)
        {
            Post result = (from p in db.Posts
                           where p.PostID == reportPost.post.PostID
                           select p).SingleOrDefault();

            result.IsReported = reportPost.post.IsReported;

            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult Delete(PostsViewModel deletedPost)
        {

            Post deletePost = (from p in db.Posts
                           where p.PostID == deletedPost.post.PostID
                           select p).SingleOrDefault();

            foreach (PostMedia m in deletePost.PostMedias.ToList())
            {
                db.PostMedias.Remove(m);
            }
            //db.SaveChanges();

            foreach (PostComment c in deletePost.PostComments.ToList())
            {
                db.PostComments.Remove(c);
            }
            //db.SaveChanges();

            db.Posts.Remove(deletePost);

            db.SaveChanges();

            return Redirect("~/Dashboard/");
        }

        [HttpPost]
        public ActionResult reportPost(PostModel reportedPost)
        {
            Post result = (from p in db.Posts
                           where p.PostID == reportedPost.PostID
                           select p).SingleOrDefault();

            result.IsReported = reportedPost.IsReported;

            db.SaveChanges();

            return View("Index");
        }

        [HttpPost]
        public ActionResult changePostActiveStatus(PostModel statusPost)
        {
            Post result = (from p in db.Posts
                           where p.PostID == statusPost.PostID
                           select p).SingleOrDefault();

            result.IsActive = statusPost.IsActive;

            db.SaveChanges();
            return View("Index");
        }

        [HttpPost]
        public ActionResult adminUpdatePost(PostModel updatedPost)
        {
            Post result = (from p in db.Posts
                           where p.PostID == updatedPost.PostID
                           select p).SingleOrDefault();

            result.PostTitle = updatedPost.PostTitle;
            result.PostContents = updatedPost.PostContents;
            result.IsActive = updatedPost.IsActive;
            result.IsReported = updatedPost.IsReported;
            result.PostedByID = updatedPost.PostedByID;
            result.PostedToID = updatedPost.PostedToID;

            db.SaveChanges();
            return View("Index");
        }

        //Your Model
        public class PostModel
        {
            [Required(ErrorMessage = "PostTitle is required")]
            public string PostTitle { set; get; }

            [Required(ErrorMessage = "PostContents is Required")]
            public string PostContents { set; get; }

            public long PostedToID { get; set; }

            public long PostedByID { get; set; }

            public long PostID { get; set; }

            public long MediaID { get; set; }

            public bool IsActive { get; set; }

            public bool IsReported { get; set; }

            public PostModel() { }
        }
    }
}