using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;
using ZenithApp1.Models;

namespace ZenithApp1.Controllers
{
    public class FlightBookingController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Book(FlightBookViewModel viewModel)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = $"DECLARE @seat int = {viewModel.Seats}; UPDATE [FlightBookings] SET [SeatsAvailability] = [SeatsAvailability] - @seat WHERE [FlightId] = {viewModel.Id}";
                    sqlCommand.ExecuteNonQuery();                    
                }
            }

            return this.Json(new { FlightId = viewModel.Id, Message = $"Booked {viewModel.Seats} seat(s) for Flight number {viewModel.Id}." });
        }

        [HttpPost]
        public ActionResult AddFlight(string From, string To, int SeatsAvailable)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = $"INSERT INTO FlightBookings ([From], [To], [SeatsAvailability]) VALUES ('{From}', '{To}', '{SeatsAvailable}')";
                    sqlCommand.ExecuteNonQuery();
                }
            }

            return this.Json("Added flight");
        }

        [HttpPost]
        public ActionResult RemoveFlight(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = $"DELETE FROM FlightBookings WHERE FlightId = '{id}'";
                    sqlCommand.ExecuteNonQuery();
                }
            }

            return this.Json("Removed flight");
        }
    }
}