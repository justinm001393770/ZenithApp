using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models.APIModels.Twitch
{
    public class Game
    {
        public int id { get; set; }
        public string name { get; set; }
        public string box_art_url { get; set; }
    }
}