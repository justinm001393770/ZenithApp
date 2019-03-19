using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;

namespace ZenithApp1.ViewModels.Games
{
    public class GamesViewModel
    {
        public IEnumerable<DBGame> gamesList { get; set; }
    }
}