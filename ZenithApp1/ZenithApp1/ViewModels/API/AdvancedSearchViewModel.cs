using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;

namespace ZenithApp1.ViewModels.API
{
    public class AdvancedSearchViewModel
    {
        public AdvancedSearch advSearch { get; set; }
        public IList<Models.Game> results { get; set; }
    }
}