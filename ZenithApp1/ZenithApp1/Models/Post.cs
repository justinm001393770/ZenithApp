namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Data.Entity.Spatial;
    using ZenithApp1.Models;
    using ZenithApp1.ViewModels.Dashboard;

    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ZenithContext db = new ZenithContext();
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostInteractions = new HashSet<PostInteraction>();
            PostMedias = new HashSet<PostMedia>();
        }

        public long PostID { get; set; }

        public long PostedToID { get; set; }

        public long PostedByID { get; set; }

        [StringLength(1000)]
        public string PostTitle { get; set; }

        [Required(ErrorMessage = "Post Text is Required")]
        public string PostContents { get; set; }

        public bool IsActive { get; set; }

        public bool IsReported { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostComment> PostComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostInteraction> PostInteractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostMedia> PostMedias { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
        public static List<Post> getPosts1()
        {
            ZenithContext dbs = new ZenithContext();
            IEnumerable<Post> posts = dbs.Posts.Where(post => post.PostedByID == 6);
            var postlist = posts.ToList();
            return postlist;
        }

        public static Post getPostById(int id)
        {
            ZenithContext dbs = new ZenithContext();
            
                //id = 5;
            
            Post postByID = dbs.Posts.Where(post => post.PostID == id).Take(1).Single();
            return postByID;
        }

        public static List<Post> getPostsFollowed(long userID)
        {
            ZenithContext dbs = new ZenithContext();

            IEnumerable<Post> posts = (from p in dbs.Posts
                                       join f in dbs.Followings on p.PostedByID equals f.FolloweeID
                                       where f.FollowerID == userID || f.FolloweeID == userID
                                       select p ).Take(10).Distinct().OrderByDescending(x => x.CreatedDate);
            var postlist = posts.ToList();
            return postlist;
        }

        public static IQueryable<Post> getPostsFollowedI(long userID)
        {
            ZenithContext dbs = new ZenithContext();

            IQueryable<Post> posts = (from p in dbs.Posts
                         where p.PostedByID == userID
                         select p).Union(from p in dbs.Posts
                                         where p.PostedToID == userID
                                         select p).Take(1000).Distinct().OrderByDescending(x => x.PostID);

            return posts;
        }

        public static IQueryable<Post> getPostsDash(long userID)
        {
            ZenithContext dbs = new ZenithContext();

            IQueryable<Post> posts = (from p in dbs.Posts
                         join f in dbs.Followings on p.PostedByID equals f.FolloweeID
                         where f.FollowerID == userID || f.FolloweeID == userID
                         select p).Union(from p in dbs.Posts
                                         where p.PostedToID == userID
                                         select p).Union(from p in dbs.Posts
                                                         where p.PostedByID == userID
                                                         select p).Take(800).Distinct().OrderByDescending(x => x.PostID);
           
            return posts;
        }

        public static IQueryable<Post> getPostsExplore(long userID)
        {
            ZenithContext dbs = new ZenithContext();

            var innerQuery = (from inQ in dbs.Followings where inQ.FollowerID == LoggedInUser.UserID select inQ.FolloweeID).Union(
                from inQ in dbs.Followings where inQ.FollowerID == LoggedInUser.UserID select inQ.FollowerID).Union(
                from U in dbs.Users where U.UserID == LoggedInUser.UserID select U.UserID).Distinct();

            IQueryable<Post> posts = (from p in dbs.Posts
                                      where !innerQuery.Contains(p.PostedByID)
                                      select p).Union(from q in dbs.Posts
                                                      where !innerQuery.Contains(q.PostedToID)
                                                      select q).Take(800).Distinct().OrderByDescending(x => x.PostID);

            return posts;
        }


   
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
                Post newPost = new Post();
                newPost.PostedByID = createPost.post.PostedByID;
                newPost.PostedToID = createPost.post.PostedByID;
                newPost.PostTitle = createPost.post.PostTitle;
                newPost.PostContents = createPost.post.PostContents;
                newPost.IsActive = createPost.post.IsActive;
                newPost.IsReported = createPost.post.IsReported;
                if (newPost.PostedByID == 0)
                    newPost.PostedByID = 6;
                if (newPost.PostedToID == 0)
                    newPost.PostedToID = 6;

                db.Posts.Add(newPost);
                db.SaveChanges();

                return View("Index");
            }

            [HttpPost]
            public ActionResult Update(Post updatedPost)
            {
                Post result = (from p in db.Posts
                               where p.PostID == updatedPost.PostID
                               select p).SingleOrDefault();

                result.PostTitle = updatedPost.PostTitle;
                result.PostContents = updatedPost.PostContents;

                db.SaveChanges();

                return View("Index");
            }

            [HttpPost]
            public ActionResult reportPost(Post reportedPost)
            {
                Post result = (from p in db.Posts
                               where p.PostID == reportedPost.PostID
                               select p).SingleOrDefault();

                result.IsReported = reportedPost.IsReported;

                db.SaveChanges();

                return View("Index");
            }

            [HttpPost]
            public ActionResult changePostActiveStatus(Post statusPost)
            {
                Post result = (from p in db.Posts
                               where p.PostID == statusPost.PostID
                               select p).SingleOrDefault();

                result.IsActive = statusPost.IsActive;

                db.SaveChanges();
                return View("Index");
            }

            [HttpPost]
            public ActionResult adminUpdatePost(Post updatedPost)
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

            public ActionResult getPosts2()
            {
                IEnumerable<Post> posts = db.Posts.Where(post => post.PostedByID == 6);
                var postlist = posts.ToList();
                return View(postlist);
            }
    }
    }
}
