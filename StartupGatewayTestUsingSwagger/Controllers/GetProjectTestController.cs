//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using StartupGateway.BusinessEntities;
//using StartupGateway.BusinessLogic;

//namespace StartupGatewayTestUsingSwagger.Controllers
//{
//    /// <summary>
//    /// Controller for managing projects via API endpoints.
//    /// </summary>
//    [ApiController]
//    [Route("api/[controller]/[action]")]
//    public class ProjectController : ControllerBase
//    {
//        private readonly ProjectBLL logicLayer;

//        public ProjectController(ProjectBLL logicLayer)
//        {
//            this.logicLayer = logicLayer;
//        }

//        /// <summary>
//        /// Get a project by its ID.
//        /// </summary>
//        /// <param name="projectId">The ID of the project to retrieve.</param>
//        /// <returns>The project with the specified ID.</returns>
//        [HttpGet]
//        public IActionResult GetProjectById([FromQuery] int projectId)
//        {
//            try
//            {
//                Project project = logicLayer.GetProjectById(projectId);

//                if (project != null)
//                {
//                    return Ok(new { project });
//                }
//                else
//                {
//                    return NotFound("Project: No project found.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or handle it accordingly
//                return StatusCode(500, "Project: Internal Server Error" + ex);
//            }
//        }

//        /// <summary>
//        /// Get a project by its name.
//        /// </summary>
//        /// <param name="projectName">The name of the project to retrieve.</param>
//        /// <returns>The project with the specified name.</returns>
//        [HttpGet]
//        public IActionResult GetProjectByName([FromQuery] string projectName)
//        {
//            try
//            {
//                Project project = logicLayer.GetProjectByName(projectName);

//                if (project != null)
//                {
//                    return Ok(new { project });
//                }
//                else
//                {
//                    return NotFound("Project: No project found.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or handle it accordingly
//                return StatusCode(500, "Project: Internal Server Error" + ex);
//            }
//        }

//        /// <summary>
//        /// Get all projects.
//        /// </summary>
//        /// <returns>A list of all projects.</returns>
//        [HttpGet]
//        public IActionResult GetAllProject()
//        {
//            try
//            {
//                List<Project> project = logicLayer.GetAllProjects();

//                if (project != null)
//                {
//                    return Ok(new { project });
//                }
//                else
//                {
//                    return NotFound("Project: No project found.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or handle it accordingly
//                return StatusCode(500, "Project: Internal Server Error" + ex);
//            }
//        }

//        /// <summary>
//        /// Add a new project.
//        /// </summary>
//        /// <param name="project">The project data to add.</param>
//        /// <returns>A success message if the project is added successfully.</returns>
//        [HttpPost]
//        public IActionResult AddaProject([FromQuery] Project project)
//        {
//            try
//            {
//                logicLayer.AddProject(project);
//                return Ok("Project added successfully");
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or handle it accordingly
//                return StatusCode(500, "Project: Internal Server Error" + ex);
//            }
//        }

//        /// <summary>
//        /// Update an existing project.
//        /// </summary>
//        /// <param name="project">The updated project data.</param>
//        /// <returns>A success message if the project is updated successfully.</returns>
//        [HttpPost]
//        public IActionResult UpdateProject([FromQuery] Project updatedProject)
//        {
//            try
//            {
//                var result = logicLayer.UpdateProject(updatedProject);
//                if (result is Project updatedResult)
//                {
//                    return Ok("Project updated successfully" + JsonConvert.SerializeObject(result));
//                }
//                else if (result is Exception ex)
//                {
//                    // Log the exception or handle it accordingly
//                    return StatusCode(500, "Unexpected response : " + ex.Message);
//                }
//                else
//                {
//                    // Handle unexpected result
//                    return StatusCode(500, " Unexpected result from UpdateProject");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log the exception or handle it accordingly
//                return StatusCode(500, "Project: Internal Server Error" + ex.Message);
//            }
//        }

//    }
//}
