using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(SignalRRealTimeSQL.Startup))]

namespace SignalRRealTimeSQL
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration { EnableJSONP = true };
                map.RunSignalR(hubConfiguration);
            });

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"SELECT [UserId],[UserName],[Email],[NIK] FROM [dbo].[T_MsUser]", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;
                    SqlDependency.Start(ConfigurationManager.ConnectionStrings["DataBase"].ConnectionString);
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == System.Data.ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listData = reader.Cast<IDataRecord>()
                          .Select(x => new Products()
                          {
                              UserID = x.GetString(0),
                              UserName = x.GetString(1),
                              Email = x.GetString(2),
                              NIK = x.GetString(3)
                          }).ToList();
                }
            }

        }
        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            var hubConnection = "http://192.168.6.232:2208/signalrservices/signalr";
        }
    }
}
