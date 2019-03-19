using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Collections;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;
using ZenithApp1.ViewModels.Game;
namespace ZenithApp1.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        // GET: Game
        public ActionResult Index(int id = 0)
        {
            var api = new APILogicV3();
            GameViewModel gvm = new GameViewModel();
            if (id == 0)
            {
                return Redirect("/API");
            }
            gvm.id = id;
            gvm.game = api.getDBGameByID(id);
            return View(gvm);
        }

        [HttpPost]
        public ActionResult Index(GameViewModel gvm)
        {
            ZenithContext db = new ZenithContext();
            using (var transaction = db.Database.BeginTransaction())
            {
                DBGame game = gvm.game;

                if (DBMethods.gameCheckIfGameExists(Int32.Parse(game.GameID.ToString())) == false)
                {
                    DBMethods.gameAddGameToDB(game);
                }

                GameLibrary library = new GameLibrary();
                library.GameID = gvm.game.GameID;
                library.UserID = LoggedInUser.UserID;
                library.Active = true;
                library.CreatedDate = DateTime.Now;
                library.UpdatedDate = null;

                if(DBMethods.gameLibraryCheckIfLibraryExists(Int32.Parse(library.UserID.ToString()), Int32.Parse(library.GameID.ToString())) == false)
                {
                    DBMethods.gameLibraryAddGameLibraryToDB(library);
                }
            }
            
            return Redirect("/Game/Index/" + gvm.game.GameID);
            //Response.Redirect("~/Dashboard");
            //return View(gvm);
        }

        
    }
}