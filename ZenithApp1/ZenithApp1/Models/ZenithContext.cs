namespace ZenithApp1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ZenithContext : DbContext
    {
        public ZenithContext()
            : base("name=ZenithConnStr")
        {
        }

        public virtual DbSet<Banned> Banneds { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<ChatText> ChatTexts { get; set; }
        public virtual DbSet<ChatUser> ChatUsers { get; set; }
        public virtual DbSet<Following> Followings { get; set; }
        public virtual DbSet<GameLibrary> GameLibraries { get; set; }
        public virtual DbSet<Interaction> Interactions { get; set; }
        public virtual DbSet<Medium> Media { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageText> MessageTexts { get; set; }
        public virtual DbSet<MessageUser> MessageUsers { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }
        public virtual DbSet<PostInteraction> PostInteractions { get; set; }
        public virtual DbSet<PostMedia> PostMedias { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Salt> Salts { get; set; }
        public virtual DbSet<SecurityQuestion> SecurityQuestions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Verification> Verifications { get; set; }
        public virtual DbSet<DBGame> Game { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chat>()
                .HasMany(e => e.ChatTexts)
                .WithRequired(e => e.Chat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Chat>()
                .HasMany(e => e.ChatUsers)
                .WithRequired(e => e.Chat)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Interaction>()
                .HasMany(e => e.PostInteractions)
                .WithRequired(e => e.Interaction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medium>()
                .HasMany(e => e.PostComments)
                .WithRequired(e => e.Medium)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medium>()
                .HasMany(e => e.PostMedias)
                .WithRequired(e => e.Medium)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Medium>()
                .HasMany(e => e.Users)
                .WithOptional(e => e.Medium)
                .HasForeignKey(e => e.ProfilePicMediaID);

            modelBuilder.Entity<Message>()
                .HasMany(e => e.MessageTexts)
                .WithRequired(e => e.Message)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .HasMany(e => e.MessageUsers)
                .WithRequired(e => e.Message)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.PostComments)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.PostInteractions)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.PostMedias)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Banneds)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Chats)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatTexts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.PosterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatUsers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Followings)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.FollowerID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Followings1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.FolloweeID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.GameLibraries)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Media)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.CreatorID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.MessageTexts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.PosterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.MessageUsers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.PostedByID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.PostedToID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.SecurityQuestions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Verifications)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
