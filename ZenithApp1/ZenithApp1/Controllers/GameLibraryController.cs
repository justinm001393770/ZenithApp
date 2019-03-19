using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;
using ZenithApp1.ViewModels.GameLibrary;

namespace ZenithApp1.Controllers
{
    public class GameLibraryController : Controller
    {
        // GET: GameLibrary
        public ActionResult Index()
        {
            int userID = LoggedInUser.UserID;
            if (userID == 0)
            {
                Response.Redirect("~/Login");
            }
            ZenithContext db = new ZenithContext();
            APILogicV3 api = new APILogicV3();
            GameLibraryViewModel gvm = new GameLibraryViewModel();
            gvm.gameLibraries = db.GameLibraries.Where(gls => gls.UserID == userID);
            //var queryable = db.Game.Where(g => g.Active == true);
            long[] gameIDs = new long[gvm.gameLibraries.Count()];
            int i = 0;
            foreach(GameLibrary gl1 in gvm.gameLibraries)
            {
                gameIDs[i] = gl1.GameID;
                i++;
                //queryable = queryable.Where(g => g.GameID == gl1.GameID);
                //Have to do entire pull at same time, TODO fix this
                //gvm.games.Add(db.Game.Where(g => g.GameID == gl1.GameID).ToList());
                //gvm.games.Concat();
            }
            if(gameIDs.Count() > 0)
            {
                gvm.games = DBMethods.getMultipleGamesByID(gameIDs);
            }
            return View(gvm);
        }
    }
}