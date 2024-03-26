using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StartupGateway.UoW.Interfaces;

namespace StartupGateway.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork) 
        { 
            _unitOfWork = unitOfWork;
        }

        //[HttpGet]
        //public IActionResult GetUserById(int userId) 
        //{ 
            
        //}
    }
}
