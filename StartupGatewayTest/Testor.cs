

using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using MySqlConnector;

namespace StartupGateway.Testing
{
	public class Tester
	{
        
        public static void Main(string[] args)
        {
            Debug.WriteLine("Started");
            //CreateHostBuilder(args).Build().Run();
            //_config = new ConfigurationManager();
            ///z
            var config = new ConfigurationBuilder()
                .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            using (var connection = new MySqlConnection(connectionString))
            {
                try
               {
                    connection.Open();
                    Debug.WriteLine("Succes");
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine("Fail");
                }
            }
        }

        
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder.UseStartup<IStartup>();
        //        });
        
        
    };
}

