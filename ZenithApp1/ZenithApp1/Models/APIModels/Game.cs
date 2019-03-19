using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models
{
    public class Game
    {
        public int id { get; set; }
        public int[] age_ratings { get; set; }
        public decimal aggregated_rating { get; set; }
        public int aggregated_rating_count { get; set; }
        public int[] alternative_names { get; set; }
        public int[] artworks { get; set; }
        public int category { get; set; }
        public int collection { get; set; }
        public int cover { get; set; }
        public Int64 created_at { get; set; }
        public int[] expansions { get; set; }
        public int[] external_games { get; set; }
        public Int64 first_release_date { get; set; }
        public int franchise { get; set; }
        public int[] franchises { get; set; }
        public int[] game_engines { get; set; }
        public int[] game_modes { get; set; }
        public int[] genres { get; set; }
        public int hypes { get; set; }
        public int[] involved_companies { get; set; }
        public Int64[] keywords { get; set; }
        public string name { get; set; }
        public int[] platforms { get; set; }
        public int[] player_perspectives { get; set; }
        public decimal popularity { get; set; }
        public decimal rating { get; set; }
        public int rating_count { get; set; }
        public Int64[] release_dates { get; set; }
        public int[] screenshots { get; set; }
        public int[] similar_games { get; set; }
        public string slug { get; set; }
        public string storyline { get; set; }
        public string summary { get; set; }
        public Int64[] tags { get; set; }
        public int[] themes { get; set; }
        public decimal total_rating { get; set; }
        public int total_rating_count { get; set; }
        public Int64 updated_at { get; set; }
        public string url { get; set; }
        public int[] videos { get; set; }
        public int[] websites { get; set; }
    }
}