using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.ViewModels.API;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;

namespace ZenithApp1.Controllers
{
    public class AdvancedSearchController : Controller
    {
        // GET: AdvancedSearch
        public ActionResult Index()
        {
            AdvancedSearchViewModel viewModel = new AdvancedSearchViewModel();
            

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(AdvancedSearchViewModel viewModel)
        {
            APILogicV3 api = new APILogicV3();
                //Formats text for the API
                viewModel.advSearch = APILogicV3.formatSearchToSlug(viewModel.advSearch);
                viewModel.results = api.advancedSearch(viewModel.advSearch);
              
            return View(viewModel);
        }
    }
}