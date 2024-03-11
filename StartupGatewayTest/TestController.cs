using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StartupGateway.Testing
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        // GET: /<controller>
        public  TestController()
        {
         
        }

        [HttpGet]
        public IActionResult Index() {
            var config = new ConfigurationBuilder()
              .Build();
            string connectionString = config.GetConnectionString("DefaultConnection");
            using (var connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return Ok("SuccessFul");
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine("Fail");
                    return Ok("Fail");
                }
            }
        }
    }
}

