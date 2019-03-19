using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models.APIModels
{
    public class AgeRating
    {
        public int id { get; set; }
        public enum categoryEnum { ESRB = 1, PEGI }
        public enum ratingEnum { Three = 1, Seven, Twelve, Sixteen, Eighteen, RP = 1, EC, E, E10, T, M, AO }
        public categoryEnum category { get; set; }
        public Int64[] content_descriptions { get; set; }
        public ratingEnum rating { get; set; }
        public string rating_cover_url { get; set; }
        public string synopsis { get; set; }

    }
}