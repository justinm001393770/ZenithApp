namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using ZenithApp1.Models;

    public partial class PostComment
    {
        public long PostCommentID { get; set; }

        public long PostID { get; set; }

        public long MediaID { get; set; }

        [Required]
        [StringLength(4000)]
        public string Comment { get; set; }

        public bool IsActive { get; set; }

        public bool IsReported { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual Medium Medium { get; set; }

        public virtual Post Post { get; set; }

        public long PostedByID { get; set; }

        public static IQueryable<PostComment> getPostComments(int postID)
        {
            ZenithContext dbs = new ZenithContext();

            IQueryable<PostComment> posts = (from p in dbs.PostComments
                                             where p.PostID == postID
                                             select p).Take(800).Distinct().OrderByDescending(x => x.PostCommentID);

            return posts;
        }

    }
}
