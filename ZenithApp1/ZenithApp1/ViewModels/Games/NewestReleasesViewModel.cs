using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;

namespace ZenithApp1.ViewModels.Games
{
    public class NewestReleasesViewModel
    {
        public IList<GameForDB> gameList { get; set; }
        public IList<GameForDB> formattedGameList { get; set; }

    }
}