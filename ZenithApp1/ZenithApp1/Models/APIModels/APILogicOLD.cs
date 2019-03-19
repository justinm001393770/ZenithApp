using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using Newtonsoft.Json;
using System.Collections;
using ZenithApp1.Models;

namespace ZenithApp1.Models
{

    //TODO remove, keeping for reference for now
    public class APILogic
    {
        string url = "https://api-2445582011268.apicast.io";
        public IList<Game> getGameByID(int id)
        {

            WebClient client = new WebClient();
            client.Headers.Add("user-key", "0afc83903e34b08081bca8faeda6d6d9");
            client.Headers.Add("Accept", "application/json");

            var json = client.DownloadString(url + "/games/" + id);
            var test = json[0];
            //var serialized = JsonConvert.SerializeObject(json);
            var game = JsonConvert.DeserializeObject<IList<Game>>(json);
            return game;
        }

        //Returns an IList of Game objects related to the search term. No particular order is given to them
        public IList<Game> searchGames(string search)
        {
            string refinedSearch = search.Replace(" ", "+");
            IList<Game> gameList = new List<Game>();

            WebClient client = new WebClient();
            client.Headers.Add("user-key", "0afc83903e34b08081bca8faeda6d6d9");
            client.Headers.Add("Accept", "application/json");

            //var json = client.DownloadString(url + "/games/?search=" + search + "?order=popularity:desc");
            var json = client.DownloadString(url + "/games/?search=" + refinedSearch);


            //var serialized = JsonConvert.SerializeObject(json);
            var listOfIDs = JsonConvert.DeserializeObject<IList<Game>>(json);

            foreach (var id in listOfIDs)
            {
                var gameJson = client.DownloadString(url + "/games/" + id.id);
                var singleGame = JsonConvert.DeserializeObject<IList<Game>>(gameJson);
                gameList.Add(singleGame[0]);


            }

            //var json2 = client.DownloadString(url + "/games/" + game[0].id);
            //var game2 = JsonConvert.DeserializeObject<IList<Game>>(json2);

            return gameList;
        }

        public IList<Game> getMostPopular(int limit)
        {
            IList<Game> gameList = new List<Game>();

            WebClient client = new WebClient();
            client.Headers.Add("user-key", "0afc83903e34b08081bca8faeda6d6d9");
            client.Headers.Add("Accept", "application/json");

            //var json = client.DownloadString(url + "/games/?search=" + search + "?order=popularity:desc");
            var json = client.DownloadString(url + "/games/?order=popularity:desc&limit=" + limit);


            //var serialized = JsonConvert.SerializeObject(json);
            var listOfIDs = JsonConvert.DeserializeObject<IList<Game>>(json);

            foreach (var id in listOfIDs)
            {
                var gameJson = client.DownloadString(url + "/games/" + id.id);
                var singleGame = JsonConvert.DeserializeObject<IList<Game>>(gameJson);
                gameList.Add(singleGame[0]);


            }

            //var json2 = client.DownloadString(url + "/games/" + game[0].id);
            //var game2 = JsonConvert.DeserializeObject<IList<Game>>(json2);

            return gameList;
        }
    }
}