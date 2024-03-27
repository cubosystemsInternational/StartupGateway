using Microsoft.AspNetCore.Mvc;
using StartupGateway.BusinessEntities;
using StartupGateway.BusinessLogic;

namespace StartupGatewayTestUsingSwagger.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserControllers: ControllerBase
    {
        private readonly UserBLL logicalLayer;

        public UserControllers(UserBLL logicalLayer)
        {
            this.logicalLayer = logicalLayer;
        }

        [HttpPost]
        public IActionResult AddaUser([FromBody] User user)
        {
            try
            {
                logicalLayer.AddUser(user);
                return Ok("Project added successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Project: Internal Server Error" + ex);
            }
        }

    }
}
