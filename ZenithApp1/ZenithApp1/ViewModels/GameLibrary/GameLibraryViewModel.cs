using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;

namespace ZenithApp1.ViewModels.GameLibrary
{
    public class GameLibraryViewModel
    {
        public IEnumerable<Models.GameLibrary> gameLibraries { get; set; }
        public IEnumerable<DBGame> games { get; set; }
        public IEnumerable<Cover> covers { get; set; }
    }
}