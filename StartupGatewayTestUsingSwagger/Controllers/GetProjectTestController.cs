using Microsoft.AspNetCore.Mvc;
using StartupGateway.BusinessEntities;
using StartupGateway.BusinessEntities.ReqModels;
using StartupGateway.BusinessLogic;

namespace StartupGatewayTestUsingSwagger.Controllers
{

        [ApiController]
        [Route("api/[controller]/[action]")]
        public class ProjectController : ControllerBase
        {
            private readonly ProjectBLL logicLayer;

            public ProjectController(ProjectBLL logicLayer)
            {
                this.logicLayer = logicLayer;
            }

            [HttpGet]
            public IActionResult GetProjectById([FromQuery] int projectId)
            {
                try
                {
                    
                    Project project = logicLayer.GetProjectById(projectId);

                    if (project != null)
                    {

                        return Ok(new { project });
                    }
                    else
                    {
                        return NotFound("Project: No project found.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception or handle it accordingly
                    return StatusCode(500, "Project: Internal Server Error" +ex);
                }
            }

        [HttpGet]
        public IActionResult GetProjectByName([FromQuery] String projectName)
        {
            try
            {

                Project project = logicLayer.GetProjectByName(projectName);

                if (project != null)
                {

                    return Ok(new { project });
                }
                else
                {
                    return NotFound("Project: No project found.");
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Project: Internal Server Error" + ex);
            }
        }

        [HttpGet]
        public IActionResult GetAllProject()
        {
            try
            {

                List<Project> project = logicLayer.GetAllProjects();

                if (project != null)
                {

                    return Ok(new { project });
                }
                else
                {
                    return NotFound("Project: No project found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Project: Internal Server Error" + ex);
            }
        }

        [HttpPost]
        public IActionResult AddaProject([FromQuery] CreateProjectModel project)
        {
            try
            {

                logicLayer.AddProject(project);
                return Ok("project added successfully");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "Project: Internal Server Error" + ex);
            }
        }


    }

    
}
