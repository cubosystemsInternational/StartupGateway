using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing projects.
    /// </summary>
    public class ProjectBLL
    {
        private readonly IProjectDAL<Project> projectsDal;
        private readonly ILogger<ProjectBLL> logger; // Logger object for logging information

        /// <summary>
        /// Constructor to initialize ProjectBLL with necessary dependencies.
        /// </summary>
        /// <param name="projectsDal">Data access layer for projects.</param>
        /// <param name="logger">Logger for logging information.</param>
        public ProjectBLL(IProjectDAL<Project> projectsDal, ILogger<ProjectBLL> logger)
        {
            this.projectsDal = projectsDal;
            this.logger = logger; // Initializing the logger object in the constructor
        }

        /// <summary>
        /// Retrieve a project by its ID.
        /// </summary>
        /// <param name="projectId">The ID of the project to retrieve.</param>
        /// <returns>The project with the specified ID.</returns>
        public Project GetProjectById(int projectId)
        {
            return projectsDal.GetProjectById(projectId);
        }

        /// <summary>
        /// Retrieve a project by its name.
        /// </summary>
        /// <param name="projectName">The name of the project to retrieve.</param>
        /// <returns>The project with the specified name.</returns>
        public Project GetProjectByName(string projectName)
        {
            return projectsDal.GetProjectByName(x => x.ProjectName == projectName);
        }

        /// <summary>
        /// Retrieve all projects.
        /// </summary>
        /// <returns>A list of all projects in the database.</returns>
        public List<Project> GetAllProjects()
        {
            return (List<Project>)projectsDal.GetAllProjects();
        }

        /// <summary>
        /// Add a new project.
        /// </summary>
        /// <param name="project">The project to add.</param>
        /// <returns>True if the project was added successfully; otherwise, false.</returns>
        public bool AddProject(Project project)
        {
            // Add the project to the database

            projectsDal.AddEntity(project);
            projectsDal.CommitChanges(); // Commit changes to the database

            // Log information about the added project
            logger.LogInformation("Project added successfully: {ProjectName}.", project.ProjectName);
            return true; // Return true indicating successful addition
        }

        /// <summary>
        /// Update an existing project.
        /// </summary>
        /// <param name="updatedProject">The updated project information.</param>
        /// <returns>The updated project if successful; otherwise, null.</returns>
        public Project UpdateProject(Project updatedProject)
        {
            // Get the existing project from the database
            var existingProject = projectsDal.GetProjectById(updatedProject.Projectid);
            if (existingProject != null) // If the project exists
            {
                // Update the project details
                existingProject.ProjectName = updatedProject.ProjectName;
                existingProject.ProjectTitle = updatedProject.ProjectTitle;
                existingProject.ProjectDescription = updatedProject.ProjectDescription;
                existingProject.ProjectValuation = updatedProject.ProjectValuation;
                existingProject.ModifiedAt = updatedProject.ModifiedAt;
                existingProject.ModifiedBy = updatedProject.ModifiedBy;

                // Update the project in the database
                projectsDal.UpdateProject(existingProject);
                projectsDal.CommitChanges(); // Commit changes to the database
                return existingProject; // Return the updated project
            }

            return null; // Project not found, return null
        }
    }
}
