using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models.APIModels.Twitch;
using RestSharp;
using Newtonsoft.Json;

namespace ZenithApp1.Models.APIModels
{
    public class APITwitchLogic
    {
        public Data getMostPopular(int limit)
        {
            string url = "https://api.twitch.tv/helix/streams?first=" + limit + "&language=en";
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "6e9df2e9-1835-4955-ae41-1c51d645080a");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Client-ID", "kc20gy5ps9izq8t0v8axmt76bi5bra");
            IRestResponse response = client.Execute(request);
            

            Data streams;
            try
            {
                streams = JsonConvert.DeserializeObject<Data>(response.Content);
            }
            catch (Exception)
            {
                streams = null;
            }
            return streams;
        }

        public Data getByUsername(string username)
        {
            string url = "https://api.twitch.tv/helix/streams?user_login=" + username;
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "c73c75eb-1d2f-4ce8-8663-413f71ee53bc");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Client-ID", "kc20gy5ps9izq8t0v8axmt76bi5bra");
            IRestResponse response = client.Execute(request);

            Data stream = JsonConvert.DeserializeObject<Data>(response.Content);
            return stream;
        }

        //This has to be an exact match between the names, only use programmatically not as a search
        public long getGameIDByName(string name)
        {
            string cleanName = name.Replace(" ", "%20");
            string url = "https://api.twitch.tv/helix/games?name=" + cleanName;
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "94704cc3-ef08-4642-9719-794332f53e2d");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Client-ID", "kc20gy5ps9izq8t0v8axmt76bi5bra");
            IRestResponse response = client.Execute(request);

            Data returnValue = JsonConvert.DeserializeObject<Data>(response.Content);
            return returnValue.data[0].id;
        }
    }
}