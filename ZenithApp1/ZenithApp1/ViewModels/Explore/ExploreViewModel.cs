using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;

namespace ZenithApp1.ViewModels.Explore
{
    public class ExploreViewModel
    {
        public IEnumerable<Post> posts { get; set; }
        public IEnumerable<Medium> medias { get; set; }
    }
}