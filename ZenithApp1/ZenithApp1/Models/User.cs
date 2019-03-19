namespace ZenithApp1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;
    using ViewModels;

    public partial class User
    {
        public static User getUserById(long id)
        {
            ZenithContext db = new ZenithContext();
            return db.Users.Find(id);
        }

        public static string getProfilePicturePathByUserId(long id)
        {
            User getUser = getUserById(id);

            IEnumerable<string> path = from m in getUser.Media
                                       where m.MediaID == getUser.ProfilePicMediaID
                                       select m.MediaPath;

            if (path.FirstOrDefault() == null)
            {
                return "";
            }

            return path.FirstOrDefault();
        }

        public string getProfilePicture()
        {
            IEnumerable<string> path = from m in Media
                                        where m.MediaID == ProfilePicMediaID
                                        select m.MediaPath;

            if(path.FirstOrDefault() == null)
            {
                return "";
            }

            return path.FirstOrDefault();
        }

        public static List<Medium> getMedia(long usersID)
        {
            ZenithContext db = new ZenithContext();
            IEnumerable<Medium> media = from m in db.Media
                                       where m.UserID == usersID
                                       select m;
            List<Medium> mediaList = media.ToList();

            return mediaList;
        }

        public static List<Medium> getMediaTop5(long usersID)
        {
            ZenithContext db = new ZenithContext();
            IEnumerable<Medium> media = from m in db.Media
                                        where m.UserID == usersID
                                        select m;
            List<Medium> mediaList = media.Take(5).ToList();

            return mediaList;
        }

        public static bool usernameExists(string username)
        {
            ZenithContext db = new ZenithContext();
            int count = db.Users.Where(x => x.UserName == username).Count();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool emailExists(string email)
        {
            ZenithContext db = new ZenithContext();
            int count = db.Users.Where(x => x.Email == email).Count();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static User getUserByEmail(string email)
        {
            ZenithContext db = new ZenithContext();
            return db.Users.Where(a => a.Email == email).FirstOrDefault();
        }

        public static User getUserByUsername(string username)
        {
            ZenithContext db = new ZenithContext();
            return db.Users.Where(a => a.UserName == username).FirstOrDefault();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Banneds = new HashSet<Banned>();
            Chats = new HashSet<Chat>();
            ChatTexts = new HashSet<ChatText>();
            ChatUsers = new HashSet<ChatUser>();
            Followings = new HashSet<Following>();
            Followings1 = new HashSet<Following>();
            GameLibraries = new HashSet<GameLibrary>();
            Media = new HashSet<Medium>();
            Messages = new HashSet<Message>();
            MessageTexts = new HashSet<MessageText>();
            MessageUsers = new HashSet<MessageUser>();
            Posts = new HashSet<Post>();
            Posts1 = new HashSet<Post>();
            Salts = new HashSet<Salt>();
            SecurityQuestions = new HashSet<SecurityQuestion>();
            Verifications = new HashSet<Verification>();
        }

        public long UserID { get; set; }

        public long? ProfilePicMediaID { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [StringLength(256)]
        public string Phone { get; set; }

        public bool Active { get; set; }

        public bool IsBanned { get; set; }

        [StringLength(256)]
        public string FirstName { get; set; }


        [StringLength(256)]
        public string LastName { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? LastLoginDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Banned> Banneds { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chat> Chats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatText> ChatTexts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatUser> ChatUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Following> Followings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Following> Followings1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GameLibrary> GameLibraries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Medium> Media { get; set; }

        public virtual Medium Medium { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MessageText> MessageTexts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MessageUser> MessageUsers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Salt> Salts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SecurityQuestion> SecurityQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Verification> Verifications { get; set; }
    }
}
