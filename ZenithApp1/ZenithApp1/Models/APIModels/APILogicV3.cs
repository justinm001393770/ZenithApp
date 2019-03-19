using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;
using ZenithApp1.Models.APIModels;
using RestSharp;
using Newtonsoft.Json;

namespace ZenithApp1.Models.APIModels
{
    public class APILogicV3
    {
        public Game getGameByID(int id)
        {
            var client = new RestClient("https://api-v3.igdb.com/games/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "6f067134-7f27-430d-a909-c834a57853d5");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields *; where id = " + id + ";", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var game = JsonConvert.DeserializeObject<IList<Game>>(response.Content);
            return game[0];
        }

        //Returns an image ID of -1 if there is no cover
        public Cover getCoverByID(int id)
        {
            var client = new RestClient("https://api-v3.igdb.com/covers");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "893f0268-6f93-4924-9321-959673e272b2");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields *; where id = " + id + ";", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Cover cover = new Cover();

            var coverList = JsonConvert.DeserializeObject<IList<Cover>>(response.Content);
            try
            {
                return coverList[0];
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                cover.image_id = "-1";
                return cover;
            }

        }

        public IList<Game> searchGames(string search, int limit)
        {
            var client = new RestClient("https://api-v3.igdb.com/games/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "7274f6c9-46c9-4fc4-98ca-fbf1d62642a2");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields *; search \"" + search + "\"; limit " + limit + ";", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var listOfGames = JsonConvert.DeserializeObject<IList<Game>>(response.Content);
            return listOfGames;
        }

        //Returns a list of games sorted by most popular, the length of which is determined by the numberOfGames argument
        //Keywords argument in parameter is to remove erotic games content so that it doesn't show up without explicitly searching for it
        public IList<GameForDB> getMostPopular(int limit)
        {
            var client = new RestClient("https://api-v3.igdb.com/games/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "7094cc0c-9d42-44da-acc4-c4002fa6f649");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields name, summary, first_release_date, cover.image_id;\nsort popularity desc;\nlimit " + limit + ";\nwhere popularity > 0 & keywords != (1991);", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            IList<GameForDB> gameList = JsonConvert.DeserializeObject<IList<GameForDB>>(response.Content);
            return gameList;
        }

        public IList<Game> getByGenre(string[] genres, int limit)
        {
            string searchTerms = "(" + genres[0];

            if (genres.Length > 1)
            {
                for (int i = 0; i < genres.Length; i++)
                {
                    searchTerms += "," + genres[i];
                }
            }

            searchTerms += ")";

            var client = new RestClient("https://api-v3.igdb.com/games/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "8fdc39da-1cf0-4b48-a00f-567a458f7d06");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields *;\nwhere genres = (" + searchTerms + ");\nsort popularity desc; limit " + limit + ";", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            IList<Game> gameList = JsonConvert.DeserializeObject<IList<Game>>(response.Content);
            return gameList;
        }

        public IList<Game> advancedSearch(AdvancedSearch search)
        {
            //isFirst is to determine if the where call has already been made. If it is true, it has been made. If it is false, it needs to be made
            bool isFirst = true;
            var client = new RestClient("https://api-v3.igdb.com/games/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "7274f6c9-46c9-4fc4-98ca-fbf1d62642a2");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");

            string requestString;
            if (search.limit == 0)
            {
                search.limit = 10;
            }
            requestString = "fields *;";
            if(search.searchTerm != "" && search.searchTerm != null)
            {
                requestString += " search \"" + search.searchTerm + "\";";
            }
            requestString += " limit " + search.limit + ";";

            if (search.genres != null)
            {
                if (search.genres[0] != null)
                {
                    if (search.genres[0].slug != null)
                    {
                        requestString += " where genres.slug = \"" + search.genres[0].slug + "\"";
                        //Looping through genres after the first one because we set the first one in the initial string
                        //All loops start at one because if there is only one index, we want to close the parenthasis immediatly
                        //So we manually add the first one out of the loop, and if the length is less than or equal to one we skip the loop
                        /*for (int i = 1; i < search.genres.Length; i++)
                        {
                            requestString += ", " + search.genres[i];
                        }
                        requestString += ")";*/
                        isFirst = false;
                    }
                }
            }

            //This will not work for more than one category and age rating, can be changed to work with more TODO make it work for more than 1 entry to each
            if (search.age_ratings != null)
            {
                if (search.age_ratings[0] != null)
                {
                        if (isFirst == true)
                        {
                            //When you create the age_ratings, set it to the [0] index of search.age_ratings
                            requestString += " where age_ratings.category = " + Convert.ToInt32(search.age_ratings[0].category) + " & age_ratings.rating = " + Convert.ToInt32(search.age_ratings[0].rating);
                            isFirst = false;
                        }
                        else
                        {
                            requestString += " & age_ratings.category = " + Convert.ToInt32(search.age_ratings[0].category) + " & age_ratings.rating = " + Convert.ToInt32(search.age_ratings[0].rating) + "";
                        }

                        /*for (int i = 1; i < search.age_ratings.Length; i++)
                        {
                            requestString += ", " + search.age_ratings[i].id;
                        }
                        requestString += ")"; */
                }
            }

            if (search.game_modes != null)
            {
                if (search.game_modes[0] != null)
                {
                    if (search.game_modes[0].slug != null)
                    {
                        if (isFirst == true)
                        {
                            requestString += " where game_modes.slug = \"" + search.game_modes[0].slug + "\"";
                            isFirst = false;
                        }
                        else
                        {
                            requestString += " & game_modes.slug = \"" + search.game_modes[0].slug + "\"";
                        }

                        /*for (int i = 1; i < search.game_modes.Length; i++)
                        {
                            requestString += ", " + search.game_modes[i];
                        }
                        requestString += ")";*/
                    }
                }
            }

            if (search.keywords != null)
            {
                if (search.keywords[0] != null)
                {

                    if (search.keywords[0].slug != null)
                    {
                        if (isFirst == true)
                        {
                            requestString += " where keywords.slug = \"" + search.keywords[0].slug + "\"";
                            isFirst = false;
                        }
                        else
                        {
                            requestString += " & keywords.slug = \"" + search.keywords[0].slug + "\"";
                        }

                        /*
                        for (int i = 1; i < search.keywords.Length; i++)
                        {
                            requestString += ", " + search.keywords[i];
                        }
                        requestString += ")";*/
                    }
                }
            }

            if (search.platforms != null)
            {
                if (search.platforms[0] != null)
                {
                    if (search.platforms[0].slug != null)
                    {
                        if (isFirst == true)
                        {
                            requestString += " where platforms.slug = \"" + search.platforms[0].slug + "\"";
                            isFirst = false;
                        }
                        else
                        {
                            requestString += " & platforms.slug = \"" + search.platforms[0].slug + "\"";
                        }


                        /*for (int i = 1; i < search.platforms.Length; i++)
                        {
                            requestString += ", " + search.platforms[i];
                        }
                        requestString += ")";*/
                    }
                }
            }

            if (search.rating != 0)
            {
                if (isFirst == true)
                {
                    requestString += " where rating >= " + search.rating;
                    isFirst = false;
                }
                else
                {
                    requestString += " & rating >= " + search.rating;
                }

            }

            /*if(search.tags.Length > 0 && search.tags[0] != 0)
            {
                if(isFirst == true)
                {
                    requestString += " where tags = (" + search.tags[0];
                    isFirst = false;
                }
                else
                {
                    requestString += " & tags = (" + search.tags[0];
                }
                
                for (int i = 1; i < search.tags.Length; i++)
                {
                    requestString += ", " + search.tags[i];
                }
                requestString += ")";
            } */

            if (isFirst == false)
            {
                requestString += ";";
            }


            //Send the request with the request string
            request.AddParameter("undefined", requestString, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var listOfGames = JsonConvert.DeserializeObject<IList<Game>>(response.Content);
            return listOfGames;
        }

        //Will only accept lowercase strings to work
        public Genre getGenreByName(string lowercaseName)
        {
            var client = new RestClient("https://api-v3.igdb.com/genres");
            var request = new RestRequest(Method.GET);

            request.AddHeader("Postman-Token", "8c8729dc-14a0-4224-bd44-9a14c0709bae");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields *;\nwhere slug = \"" + lowercaseName + "\";", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Genre genre = JsonConvert.DeserializeObject<Genre>(response.Content);
            return genre;
        }

        public AgeRating getAgeRatingByID(int id)
        {
            var client = new RestClient("https://api-v3.igdb.com/age_ratings/");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "d5aac4ec-f213-4668-ac5e-a5a54da81e8d");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields *; where id = " + id + ";", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            IList<AgeRating> ageRating = JsonConvert.DeserializeObject<IList<AgeRating>>(response.Content);
            return ageRating[0];
        }

        //TODO test this
        public static string formatStringToSlug(string text)
        {
            if (text != null)
            {
                string slug = text.Replace(" ", "-");
                slug = slug.Replace("(", "");
                slug = slug.Replace(")", "");
                slug = slug.ToLower();
                return slug;
            }
            else
            {
                return text;
            }
        }

        //Formats user inputted text into slug format for the API, use before sending AdvancedSearch item to API
        public static AdvancedSearch formatSearchToSlug(AdvancedSearch search)
        {
            if (search.game_modes != null)
            {
                if (search.game_modes[0] != null)
                {
                    if (search.game_modes[0].slug != null)
                    {
                        search.game_modes[0].slug = formatStringToSlug(search.game_modes[0].slug);
                    }
                }
            }

            if (search.keywords != null)
            {
                if (search.keywords[0] != null)
                {
                    if (search.keywords[0].slug != null)
                    {
                        search.keywords[0].slug = formatStringToSlug(search.keywords[0].slug);
                    }
                }
            }

            if (search.platforms != null)
            {
                if (search.platforms[0] != null)
                {
                    if (search.platforms[0].slug != null)
                    {
                        search.platforms[0].slug = formatStringToSlug(search.platforms[0].slug);
                    }
                }
            }

            if (search.genres != null)
            {
                if (search.genres[0] != null)
                {
                    if (search.genres[0].slug != null)
                    {
                        search.genres[0].slug = formatStringToSlug(search.genres[0].slug);
                    }
                }
            }


            return search;
        }

        public DBGame getDBGameByID(int id)
        {
            var client = new RestClient("https://api-v3.igdb.com/games");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "8ab02447-5e7a-4bab-ba6f-218cf2c8b689");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields name, summary, first_release_date, cover.image_id;\nwhere id = " + id + ";", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            IList<GameForDB> game = JsonConvert.DeserializeObject<IList<GameForDB>>(response.Content);
            DBGame result = new DBGame();
            result.GameID = game[0].id;
            result.GameName = game[0].name;
            result.GameDesc = game[0].summary;
            if(game[0].cover != null)
            {
                result.GameImgURL = game[0].cover.image_id;
            }
            result.GameReleaseDate = APILogicV3.convertUnixToDateTime(game[0].first_release_date);
            //result.Active = 1;
            //result.CreatedDate = DateTime.Now;
            return result;
        }

        public IList<GameForDB> getNewestReleases()
        {
            var client = new RestClient("https://api-v3.igdb.com/games");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Postman-Token", "642a9f32-18e3-4ebe-ae96-b8f403001d9b");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("user-key", "1039b6d01cfb3ef99ea0bc84604c53ff");
            request.AddParameter("undefined", "fields name, first_release_date, summary, cover.image_id; sort first_release_date desc; where first_release_date != null;", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            IList<GameForDB> results = JsonConvert.DeserializeObject<IList<GameForDB>>(response.Content);
            return results;
        }

        public static DateTime convertUnixToDateTime(long input)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(input).ToLocalTime();
            return dateTime;
        }


    }
}