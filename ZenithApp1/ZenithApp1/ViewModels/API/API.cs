using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZenithApp1.ViewModels.API
{
    public class APIViewModel
    {
        public IList<Models.Game> gameList { get; set; }
        //GameName and search are used for debugging to test sending data to the view, TODO remove this
        public string search { get; set; }
        //public string gameName { get; set; }
        //public IList<Models.Game> popularGames { get; set; }
    }
}