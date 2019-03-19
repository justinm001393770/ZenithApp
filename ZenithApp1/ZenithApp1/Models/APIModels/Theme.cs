using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models
{
    public class Theme
    {
        public int id { get; set; }
        public string name { get; set; }
        public Int64 created_at { get; set; }
        public Int64 updated_at { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int[] games { get; set; }
    }
}