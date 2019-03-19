using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;
using ZenithApp1.ViewModels.GameLibrary;
using ZenithApp1.ViewModels.Games;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class GamesController : Controller
    {
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
            foreach (GameLibrary gl1 in gvm.gameLibraries)
            {
                gameIDs[i] = gl1.GameID;
                i++;
                //queryable = queryable.Where(g => g.GameID == gl1.GameID);
                //Have to do entire pull at same time, TODO fix this
                //gvm.games.Add(db.Game.Where(g => g.GameID == gl1.GameID).ToList());
                //gvm.games.Concat();
            }
            if (gameIDs.Count() > 0)
            {
                gvm.games = DBMethods.getMultipleGamesByID(gameIDs);
            }
            return View(gvm);
        }

        public ActionResult ViewOthersGames(string userName)
        {
            ZenithContext db = new ZenithContext();
            GamesViewModel gvm = new GamesViewModel();
            User player = DBMethods.getUserByUsername(userName);
            IEnumerable<GameLibrary> gameLibrary;
            long[] gameIDs;
            if (player == null || player.UserID == LoggedInUser.UserID)
            {
                return RedirectToAction("Index");
            }
            gameLibrary = db.GameLibraries.Where(gl => gl.UserID == player.UserID);
            gameIDs = new long[gameLibrary.Count()];
            int i = 0;
            foreach (GameLibrary gl in gameLibrary)
            {
                gameIDs[i] = gl.GameID;
                i++;
            }
            gvm.gamesList = DBMethods.getMultipleGamesByID(gameIDs);
            return View(gvm);
        }

        [HttpGet]
        public ActionResult NewestReleases()
        {
            NewestReleasesViewModel nvm = new NewestReleasesViewModel();
            APILogicV3 api = new APILogicV3();
            nvm.gameList = api.getNewestReleases();
            return View(nvm);
        }

        [HttpPost]
        public ActionResult NewestReleases(int gameID)
        {
            return View();
        }

        [HttpGet]
        public ActionResult MostPopular()
        {
            NewestReleasesViewModel nvm = new NewestReleasesViewModel();
            APILogicV3 api = new APILogicV3();
            nvm.gameList = api.getMostPopular(10);
            return View(nvm);
        }

        [HttpPost]
        public ActionResult MostPopular(int gameID)
        {
            return View();
        }

        [HttpGet]
        public ActionResult RemoveGameFromLibrary(int gameID)
        {
            ZenithContext db = new ZenithContext();
            IEnumerable<GameLibrary> gl = db.GameLibraries.Where(a => a.GameID == gameID && a.UserID == LoggedInUser.UserID);
            foreach (GameLibrary g in gl)
            {
                try
                {
                    db.GameLibraries.Remove(g);
                }
                catch (Exception)
                {

                }

            }
            db.SaveChanges();

            return RedirectToAction("Index", "Games");
        }

        public ActionResult MoreInformation(int gameID)
        {
            return RedirectToAction("Index", "Game", new { gameID = gameID });
            //return Redirect("/Game/Index/" + gameID);
        }

        public ActionResult AddGameToLibrary(int gameID)
        {
            ZenithContext db = new ZenithContext();
            DBGame game = new DBGame();
            game.GameID = gameID;
            

            if(db.Game.Where(a => a.GameID == game.GameID).SingleOrDefault() == null)
            {
                APILogicV3 api = new APILogicV3();
                Game apiGame = api.getGameByID(gameID);
                Cover cover = api.getCoverByID(apiGame.cover);
                game.Active = true;
                game.CreatedDate = DateTime.Now;
                game.GameDesc = apiGame.summary;
                game.GameID = gameID;
                game.GameImgURL = cover.image_id;
                game.GameName = apiGame.name;
                game.GameReleaseDate = APILogicV3.convertUnixToDateTime(apiGame.first_release_date);
                game.UpdatedDate = null;
                DBMethods.gameAddGameToDB(game);
                db.SaveChanges();
            }

            GameLibrary gl = new GameLibrary();
            gl.Active = true;
            gl.CreatedDate = DateTime.Now;
            gl.GameID = gameID;
            gl.UpdatedDate = null;
            gl.UserID = LoggedInUser.UserID;

            DBMethods.gameLibraryAddGameLibraryToDB(gl);
            db.SaveChanges();
            return RedirectToAction("Index", "Games");
        }
    }
}