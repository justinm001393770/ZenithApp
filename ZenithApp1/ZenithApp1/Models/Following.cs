namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Following")]
    public partial class Following
    {
        public long FollowingID { get; set; }

        public long FollowerID { get; set; }

        public long FolloweeID { get; set; }

        public bool Active { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? UpdatedDate { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public static bool addFollower(long followID)
        {
            ZenithContext db = new ZenithContext();
            using (var transaction = db.Database.BeginTransaction())
            {
                IQueryable<Following> results = db.Followings.Where(a => a.FollowerID == LoggedInUser.UserID && a.FolloweeID == followID);
                if (results.Count() > 0)
                {
                    foreach(Following f in results)
                    {
                        f.Active = true;
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                Following follow = new Following();
                follow.FollowerID = LoggedInUser.UserID;
                follow.FolloweeID = followID;
                follow.Active = true;
                follow.CreatedDate = DateTime.Now;
                follow.UpdatedDate = null;
                db.Followings.Add(follow);
                db.SaveChanges();
                transaction.Commit();
                return true;
            }                
        }

        public static bool removeFollower(long followID)
        {
            ZenithContext db = new ZenithContext();
            using (var transaction = db.Database.BeginTransaction())
            {
                IQueryable<Following> results = db.Followings.Where(a => a.FollowerID == LoggedInUser.UserID && a.FolloweeID == followID);
                if (results.Count() > 0)
                {
                    foreach (Following f in results)
                    {
                        db.Followings.Remove(f);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                return false;
            }
        }
        public static bool removeFollowee(long followID)
        {
            ZenithContext db = new ZenithContext();
            using (var transaction = db.Database.BeginTransaction())
            {
                IQueryable<Following> results = db.Followings.Where(a => a.FollowerID == followID && a.FolloweeID == LoggedInUser.UserID);
                if (results.Count() > 0)
                {
                    foreach (Following f in results)
                    {
                        db.Followings.Remove(f);
                    }
                    db.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                return false;
            }
        }
    }
}
