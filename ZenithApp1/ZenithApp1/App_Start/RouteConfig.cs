using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ZenithApp1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("PostDataList", "GetPosts/", new { controller = "Profile", action = "GetProjects" });

            routes.MapRoute("PostCommentDataList", "GetPostComments/", new { controller = "Posts", action = "GetProjects" });

            routes.MapRoute("PostDataListDash", "GetPostsDash/", new { controller = "Dashboard", action = "GetProjects" });

            routes.MapRoute("PostDataListExplore", "GetPostsExplore/", new { controller = "Explore", action = "GetProjects" });

            routes.MapRoute(
                "UserProfileEdit",                                           // Route name
                "Profile/Edit/",                            // URL with parameters
                new { controller = "Profile", action = "Edit"}  // Parameter defaults
            );

            routes.MapRoute(
                "UserProfile",                                           // Route name
                "Profile/{userName}",                            // URL with parameters
                new { controller = "Profile", action = "Index", userName = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                "Follow",                                           // Route name
                "Follow/{userName}",                            // URL with parameters
                new { controller = "Profile", action = "Follow" }  // Parameter defaults
            );

            routes.MapRoute(
                "Unfollow",                                           // Route name
                "Unfollow/{userName}",                            // URL with parameters
                new { controller = "Profile", action = "Unfollow" }  // Parameter defaults
            );

            routes.MapRoute(
                "Unfollowee",                                           // Route name
                "Unfollowee/{userName}",                            // URL with parameters
                new { controller = "Profile", action = "Unfollowee" }  // Parameter defaults
            );

            routes.MapRoute(
                "GameSearch",                                           // Route name
                "GameSearch",                            // URL with parameters
                new { controller = "API", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(
                "PostsPageEdit",                                           // Route name
                "Posts/Edit/{postID}",                            // URL with parameters
                new { controller = "Posts", action = "Edit" }  // Parameter defaults
            );

            routes.MapRoute(
                "PostsPage",                                           // Route name
                "Posts/{postID}",                            // URL with parameters
                new { controller = "Posts", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(
                "ViewGame",                                           // Route name
                "Game/Index/{gameID}",                            // URL with parameters
                new { controller = "Game", action = "Index", gameID = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                "GamesPage",                                           // Route name
                "Games/",                            // URL with parameters
                new { controller = "Games", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(
                "GamesPageOthers",                                           // Route name
                "Games/{userName}",                            // URL with parameters
                new { controller = "Games", action = "ViewOthersGames", userName = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                "GamesNewestRelease",                                           // Route name
                "Games/Find/Newest",                            // URL with parameters
                new { controller = "Games", action = "NewestReleases" }  // Parameter defaults
            );

            routes.MapRoute(
                "GamesMostPopular",                                           // Route name
                "Games/Find/MostPopular",                            // URL with parameters
                new { controller = "Games", action = "MostPopular" }  // Parameter defaults
            );

            routes.MapRoute(
                "RemoveGame",                                           // Route name
                "Games/Remove/{gameID}",                            // URL with parameters
                new { controller = "Games", action = "RemoveGameFromLibrary", gameID = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                "MoreInformation",                                           // Route name
                "Games/MoreInformation/{gameID}",                            // URL with parameters
                new { controller = "Games", action = "MoreInformation", gameID = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                "AddGameToLibrary",                                           // Route name
                "Games/Add/{gameID}",                            // URL with parameters
                new { controller = "Games", action = "AddGameToLibrary", gameID = UrlParameter.Optional }  // Parameter defaults
            );

            routes.MapRoute(
                "FollowersPage",                                           // Route name
                "Followers/",                            // URL with parameters
                new { controller = "Followers", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(
                "OthersFollowersPage",                                           // Route name
                "Followers/{userName}",                            // URL with parameters
                new { controller = "Followers", action = "OtherFollowers" }  // Parameter defaults
            );


            routes.MapRoute(
                "FollowingPage",                                           // Route name
                "Following/",                            // URL with parameters
                new { controller = "Following", action = "Index" }  // Parameter defaults
            );

            routes.MapRoute(
                "OthersFollowingPage",                                           // Route name
                "Following/{userName}",                            // URL with parameters
                new { controller = "Following", action = "OtherFollowing" }  // Parameter defaults
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
