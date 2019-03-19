using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models
{
    public class GameEngine
    {
        public int id { get; set; }
        public string names { get; set; }
        public Int64 created_at { get; set; }
        public Int64 updated_at { get; set; }
        public object logo { get; set; }
        public string slug { get; set; }
        public string url { get; set; }
        public int[] games { get; set; }
        public int[] platforms { get; set; }
        public int[] companies { get; set; }
    }
}