using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenithApp1.Models;
using static ZenithApp1.Startup;

namespace ZenithApp1.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            //instantiate viewmodel object

            //add objects to viewmodel object

            //pass viewmodel object into view in next line
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string chatText)
        {
            if (chatText[0].ToString() == "@")
            {
                int startindex = chatText.IndexOf('@');
                int Endindex = chatText.IndexOf(' ');

                string username = chatText.Substring(startindex + 1, Endindex - startindex - 1);

                int startindex2 = chatText.IndexOf(' ');
                int Endindex2 = chatText.Length;

                string message = chatText.Substring(startindex2 + 1, Endindex2 - startindex2 - 1);

                Models.User textUser = Models.User.getUserByUsername(username);
                string phone = textUser.Phone;
                SNSController sns = new SNSController();

                sns.sendText(phone, message, LoggedInUser.UserName);

                return this.Json("Text message sent");
            }
            else
            {

                int chatId = 1;
                int posterId = LoggedInUser.UserID;
                int isActive = 1;
                DateTime createdDate = DateTime.Now;
                string posterUsername = LoggedInUser.UserName;
                string posterImagePath = LoggedInUser.UserProfileImage;

                if (posterImagePath == "")
                {
                    posterImagePath = "https://s3.amazonaws.com/zenith-user-images/blankuser4.png";
                }

                var connectionString = ConfigurationManager.ConnectionStrings["ZenithConnStr"].ConnectionString;
                using (var sqlConnection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO ChatText ([ChatID], [PosterID], [ChatText], [IsActive], [CreatedDate], [PosterUsername], [PosterImagePath]) VALUES (@chatID, @posterID, @chatText, @isActive, @createdDate, @posterUsername, @posterImagePath)";

                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        command.Parameters.AddWithValue("@chatID", chatId);
                        command.Parameters.AddWithValue("@posterID", posterId);
                        command.Parameters.AddWithValue("@chatText", chatText);
                        command.Parameters.AddWithValue("@isActive", isActive);
                        command.Parameters.AddWithValue("@createdDate", createdDate);
                        command.Parameters.AddWithValue("@posterUsername", posterUsername);
                        command.Parameters.AddWithValue("@posterImagePath", posterImagePath);

                        sqlConnection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                return this.Json("Added message");
            }
        }
    }
}