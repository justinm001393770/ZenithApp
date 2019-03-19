namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.Spatial;
    using System.IO;

    public partial class Medium
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Medium()
        {
            PostComments = new HashSet<PostComment>();
            PostMedias = new HashSet<PostMedia>();
            Users = new HashSet<User>();
        }

        public static bool isVideo(string path)
        {
            if(Path.GetExtension(path) == ".mp4")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Key]
        public long MediaID { get; set; }

        public long UserID { get; set; }

        [StringLength(256)]
        public string MediaName { get; set; }

        public string MediaPath { get; set; }

        public bool IsPublic { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostComment> PostComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostMedia> PostMedias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }

        public static IQueryable<Medium> getMediumExploreImages(long userID)
        {
            ZenithContext dbs = new ZenithContext();

            var innerQuery = (from inQ in dbs.Followings where inQ.FollowerID == LoggedInUser.UserID select inQ.FolloweeID).Union(
                from inQ in dbs.Followings where inQ.FollowerID == LoggedInUser.UserID select inQ.FollowerID).Union(
                from U in dbs.Users where U.UserID == LoggedInUser.UserID select U.UserID).Distinct();

            IQueryable<Medium> medias = (from m in dbs.Media
                                      where !innerQuery.Contains(m.UserID)
                                        && !m.MediaPath.EndsWith(".mp4")
                                      select m).Take(800).Distinct().OrderByDescending(x => x.CreatedDate);



            return medias;
        }

        public static IQueryable<Medium> getMediumExploreVideos(long userID)
        {
            ZenithContext dbs = new ZenithContext();

            var innerQuery = (from inQ in dbs.Followings where inQ.FollowerID == LoggedInUser.UserID select inQ.FolloweeID).Union(
                from inQ in dbs.Followings where inQ.FollowerID == LoggedInUser.UserID select inQ.FollowerID).Union(
                from U in dbs.Users where U.UserID == LoggedInUser.UserID select U.UserID).Distinct();

            IQueryable<Medium> medias = (from m in dbs.Media
                                         where !innerQuery.Contains(m.UserID)
                                           && m.MediaPath.EndsWith(".mp4")
                                         select m).Take(800).Distinct().OrderByDescending(x => x.CreatedDate);



            return medias;
        }
    }
}
