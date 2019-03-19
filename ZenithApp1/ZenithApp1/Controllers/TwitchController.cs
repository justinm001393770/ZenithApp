using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models.APIModels;
using ZenithApp1.Models.APIModels.Twitch;
using ZenithApp1.ViewModels.Twitch;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class TwitchController : Controller
    {
        // GET: Twitch
        [HttpGet]
        public ActionResult Index()
        {
            APITwitchLogic api = new APITwitchLogic();
            TwitchViewModel viewModel = new TwitchViewModel();

            Data data = new Data();
            data = api.getMostPopular(9);

            viewModel.streams = data.data;
            return View(viewModel);
        }
    }
}