﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.Models
{
    public class Cover
    {
        public bool alpha_channel { get; set; }
        public bool animated { get; set; }
        public int height { get; set; }
        public string image_id { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }
}