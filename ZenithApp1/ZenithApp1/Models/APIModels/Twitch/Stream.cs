using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models.APIModels.Twitch
{
    public class Stream
    {
        public Int64 id { get; set; }
        public Int64 user_id { get; set; }
        public string user_name { get; set; }
        public int? game_id { get; set; }
        public string[] community_ids { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public int viewer_count { get; set; }
        public string started_at { get; set; }
        public string language { get; set; }
        public string thumbnail_url { get; set; }
        public string[] tag_ids { get; set; }
    }
}