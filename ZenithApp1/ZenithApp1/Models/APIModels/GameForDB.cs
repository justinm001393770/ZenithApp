using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models.APIModels
{
    public class GameForDB
    {
        public int id { get; set; }
        public Cover cover { get; set; }
        public long first_release_date { get; set; }
        public string name { get; set; }
        public string summary { get; set; }
    }
}