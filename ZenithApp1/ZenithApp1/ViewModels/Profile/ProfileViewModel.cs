using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;

namespace ZenithApp1.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public User profileOwner;
        public List<Post> posts;
        public int id { get; set; }
        public Models.Post post { get; set; }
        public Models.PostComment postComment { get; set; }
        public Models.Following following { get; set; }
        public Models.Following followers { get; set; }
        public List<Medium> usersMedia { get; set; }
        public List<string> editPostMedias { get; set; }
    }
}