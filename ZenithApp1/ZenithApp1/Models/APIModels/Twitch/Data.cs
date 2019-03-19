using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models.APIModels.Twitch
{
    public class Data
    {
        public Stream[] data { get; set; }
        public Pagination pagination { get; set; }
        public Game game { get; set; }
    }
}