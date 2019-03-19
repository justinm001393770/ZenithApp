using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;
using ZenithApp1.ViewModels.API;

namespace ZenithApp1.Controllers
{
    public class APIController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var api = new APILogicV3();
            APIViewModel viewModel = new APIViewModel();
            
            //viewModel.popularGames = api.getMostPopular(10);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(string search)
        {
            var api = new APILogicV3();
            ViewModels.API.APIViewModel returnData = new ViewModels.API.APIViewModel();
            //returnData.popularGames = api.getMostPopular(10);
            if (!String.IsNullOrEmpty(search))
            {
                returnData.gameList = api.searchGames(search, 10);
            }
            return View(returnData);
        }

        

        
    }
}