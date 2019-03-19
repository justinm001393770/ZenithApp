using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using ZenithApp1.Models;

namespace ZenithApp1.ViewModels.Posts
{
    public class PostsViewModel
    {
        public int id { get; set; }
        public List<PostComment> posts;
        public Models.Post post { get; set; }
        public Models.PostComment comment { get; set; }
        public Models.User thisUser { get; set; }
        public List<Medium> usersMedia { get; set; }
        public List<string> editPostMedias { get; set; }

    }
}