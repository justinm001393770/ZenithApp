using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models.APIModels;

namespace ZenithApp1.Models.APIModels
{
    public class AdvancedSearch
    {
        public AgeRating[] age_ratings { get; set; }
        public GameMode[] game_modes { get; set; }
        public Genre[] genres { get; set; }
        public Keyword[] keywords { get; set; }
        public Platform[] platforms { get; set; }
        public double rating { get; set; }
        public Int64[] tags { get; set; }
        public string searchTerm { get; set; }
        public int limit { get; set; }
    }
}