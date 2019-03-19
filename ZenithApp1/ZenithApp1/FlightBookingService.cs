using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using ZenithApp1.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.Mappers;
using TableDependency.SqlClient;
using System.Text;

namespace ZenithApp1
{
    public class FlightBookingService : IDisposable
    {
        #region Member variables

        // Singleton instance
        private readonly static Lazy<FlightBookingService> _instance = new Lazy<FlightBookingService>(() => new FlightBookingService(GlobalHost.ConnectionManager.GetHubContext<FlightBookingHub>().Clients));
        private SqlTableDependency<FlightAvailability> SqlTableDependency { get; }
        private IHubConnectionContext<dynamic> Clients { get; }

        #endregion

        #region Constructors

        private FlightBookingService(IHubConnectionContext<dynamic> clients)
        {
            this.Clients = clients;

            // Because our C# model has a property not matching database table name, an explicit mapping is required just for this property.
            var mapper = new ModelToTableMapper<FlightAvailability>();
            mapper.AddMapping(x => x.ChatText, "ChatText");
            mapper.AddMapping(x => x.UserName, "PosterUsername");
            mapper.AddMapping(x => x.ImagePath, "PosterImagePath");

            // Because our C# model name differs from table name we have to specify database table name.
            this.SqlTableDependency = new SqlTableDependency<FlightAvailability>(ConfigurationManager.ConnectionStrings["ZenithConnStr"].ConnectionString, "ChatText", mapper);
            this.SqlTableDependency.OnChanged += this.TableDependency_OnChanged;
            //this.SqlTableDependency.TraceLevel = TraceLevel.Verbose;
            //this.SqlTableDependency.TraceListener = new TextWriterTraceListener(File.Create("c:\\Temp\\output.txt"));
            this.SqlTableDependency.Start();
        }

        #endregion

        #region Public Methods

        public static FlightBookingService Instance => _instance.Value;

        public FlightsAvailability GetAll()
        {
            var flightsAvailability = new List<FlightAvailability>();

            var connectionString = ConfigurationManager.ConnectionStrings["ZenithConnStr"].ConnectionString;
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    StringBuilder strCommand = new StringBuilder();

                    sqlCommand.CommandText = "SELECT * FROM [ChatText]";

                    using (var sqlDataReader = sqlCommand.ExecuteReader())
                    {
                        while (sqlDataReader.Read())
                        {
                            long chatTextId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ChatTextID"));
                            long chatId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("ChatID"));
                            long posterId = sqlDataReader.GetInt64(sqlDataReader.GetOrdinal("PosterID"));
                            string chatText = sqlDataReader.GetString(sqlDataReader.GetOrdinal("ChatText"));
                            string userName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PosterUsername"));
                            string imagePath = sqlDataReader.GetString(sqlDataReader.GetOrdinal("PosterImagePath"));
                            DateTime createdDate = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("CreatedDate"));

                            flightsAvailability.Add(new FlightAvailability { ChatTextId = chatTextId, ChatId = chatId, PosterId = posterId, ChatText = chatText, CreatedDate = createdDate, UserName = userName, ImagePath = imagePath });
                        }
                    }
                }
            }

            return new FlightsAvailability() { FlightCompanyId = "not used", FlightAvailability = flightsAvailability };
        }

        #endregion

        #region Private Methods

        private void TableDependency_OnChanged(object sender, RecordChangedEventArgs<FlightAvailability> e)
        {
            switch (e.ChangeType)
            {
                case ChangeType.Delete:                    
                    this.Clients.All.removeFlightAvailability(e.Entity);
                    break;

                case ChangeType.Insert:
                    this.Clients.All.addFlightAvailability(e.Entity);
                    break;

                case ChangeType.Update:
                    this.Clients.All.updateFlightAvailability(e.Entity);
                    break;
            }
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            // Invoke Stop() in order to remove all DB objects genetated from SqlTableDependency.
            this.SqlTableDependency.Stop();
        }

        #endregion
    }
}