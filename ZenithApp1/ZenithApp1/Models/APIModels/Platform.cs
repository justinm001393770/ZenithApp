using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models.APIModels
{
    public class Platform
    {
        public enum Category { console = 1, arcade, platform, operating_system, portable_console, computer }
        public int id { get; set; }
        public string abbreviation { get; set; }
        public string alternative_name { get; set; }
        public Category category { get; set; }
        public Int64 created_at { get; set; }
        public int generation { get; set; }
        public string name { get; set; }
        public int platform_logo { get; set; }
        public int product_family { get; set; }
        public string slug { get; set; }
        public string summary { get; set; }
        public Int64 updated_at { get; set; }
        public string url { get; set; }
        public int versions { get; set; }
        public int websites { get; set; }
    }
}