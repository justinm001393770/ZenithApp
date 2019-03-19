using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;

namespace ZenithApp1.ViewModels.Game
{
    public class GameViewModel
    {
        public int id { get; set; }
        public DBGame game { get; set; }
    }
}