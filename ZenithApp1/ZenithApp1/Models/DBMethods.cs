using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models;

namespace ZenithApp1.Models
{

    public class DBMethods
    {


        public static IEnumerable<DBGame> gameGetGameByID(int id)
        {
            ZenithContext db = new ZenithContext();
            IEnumerable<DBGame> games = db.Game.Where(a => a.GameID == id);
            
            return games;
        }

        //Same method as above but provides a bool for easier use in code
        public static bool gameCheckIfGameExists(int id)
        {
            ZenithContext db = new ZenithContext();
            IEnumerable<DBGame> games = db.Game.Where(a => a.GameID == id);
            if (games.Count() == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool gameAddGameToDB(DBGame game)
        {
            ZenithContext db = new ZenithContext();
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    game.CreatedDate = DateTime.Now;
                    game.Active = true;
                    db.Game.Add(game);
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Game ON");
                    //Seems to be setting the Active bit to 1, if that is no longer the case uncomment this line
                    //db.Game.SqlQuery("UPDATE Game SET Active = 'TRUE' WHERE GameID = @p0", game.GameID);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Game OFF");
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool gameRemoveGameFromDB(DBGame game)
        {
            ZenithContext db = new ZenithContext();
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    game.CreatedDate = DateTime.Now;
                    game.Active = true;
                    db.Game.Add(game);
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Game ON");
                    //Seems to be setting the Active bit to 1, if that is no longer the case uncomment this line
                    //db.Game.SqlQuery("UPDATE Game SET Active = 'TRUE' WHERE GameID = @p0", game.GameID);
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Game OFF");
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool gameLibraryCheckIfLibraryExists(int userID, int gameID)
        {
            ZenithContext db = new ZenithContext();
            using (var transaction = db.Database.BeginTransaction())
            {
                if (db.GameLibraries.Where(a => a.UserID == userID && a.GameID == gameID).Count() == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool gameLibraryAddGameLibraryToDB(GameLibrary gl)
        {
            ZenithContext db = new ZenithContext();
            try
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.GameLibraries.Add(gl);
                    db.SaveChanges();
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static IEnumerable<DBGame> getMultipleGamesByID(long[] ids)
        {
            ZenithContext db = new ZenithContext();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    string query = "SELECT * FROM Game WHERE GameID = " + ids[0];
                    for (int i = 1; i < ids.Length; i++)
                    {
                        query += " OR GameID = " + ids[i];
                    }
                    return db.Game.SqlQuery(query);
                }
                catch(Exception ex)
                {
                    return null;
                }

            }
        }

        public static IEnumerable<Post> getExplorePosts(int userID)
        {
            ZenithContext db = new ZenithContext();
            
            using (var transaction = db.Database.BeginTransaction())
            {
                String query = "SELECT * FROM Following WHERE FollowerID = " + userID; // + " OR FolloweeID = " + userID;
                IEnumerable<Following> follow = db.Followings.SqlQuery(query);
                //db.Posts.Where(p => p.PostedByID != userID);
                IEnumerable<Post> post = new List<Post>();
                String query2 = "SELECT TOP 100 * FROM Posts WHERE PostedByID != " + userID;
                foreach(Following f in follow)
                {
                    query2 += " AND PostedByID != " + f.FolloweeID; // + " AND PostedByID != " + f.FollowerID;
                }
                post = db.Posts.SqlQuery(query2);
                return post;
            }
        }

        public static User getUserByUsername(string username)
        {
            ZenithContext db = new ZenithContext();
            List<User> userList;

            using (var transaction = db.Database.BeginTransaction())
            {
                userList = db.Users.Where(d => d.UserName == username).ToList();
            }
            if(userList.Count() > 0)
            {
                return userList[0];
            }
            else
            {
                return null;
            }
        }
    }
}