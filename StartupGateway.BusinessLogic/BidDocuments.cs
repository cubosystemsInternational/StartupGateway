using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
using StartupGateway.UoW.Interfaces;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing projects.
    /// </summary>
    public class ProjectBLL
    {
        private readonly IUnitOfWork uow; 
        private readonly ILogger<ProjectBLL> logger; // Logger object for logging information

        /// <summary>
        /// Constructor to initialize ProjectBLL with necessary dependencies.
        /// </summary>
        /// <param name="projectsDal">Data access layer for projects.</param>
        /// <param name="logger">Logger for logging information.</param>
        public ProjectBLL(IUnitOfWork uow, ILogger<ProjectBLL> logger)
        {
            this.uow = uow;
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

        public object UpdateProject(Project updatedProject)
        {
            try
            {
                // Get the existing project from the database
                var existingProject = projectsDal.GetProjectById(updatedProject.ProjectId);
                if (existingProject != null) // If the project exists
                {
                    // Update the project details if the updated values are not empty
                    existingProject.ProjectName = !string.IsNullOrWhiteSpace(updatedProject.ProjectName) ? updatedProject.ProjectName : existingProject.ProjectName;
                    existingProject.ProjectTitle = !string.IsNullOrWhiteSpace(updatedProject.ProjectTitle) ? updatedProject.ProjectTitle : existingProject.ProjectTitle;
                    existingProject.ProjectDescription = !string.IsNullOrWhiteSpace(updatedProject.ProjectDescription) ? updatedProject.ProjectDescription : existingProject.ProjectDescription;
                    existingProject.ProjectValuation = updatedProject.ProjectValuation ?? existingProject.ProjectValuation;
                    existingProject.Status = updatedProject.Status;
                    existingProject.ModifiedAt = updatedProject.ModifiedAt ?? existingProject.ModifiedAt;
                    existingProject.ModifiedBy = updatedProject.ModifiedBy != null ? updatedProject.ModifiedBy : existingProject.ModifiedBy;

                    // Update the project in the database
                    projectsDal.UpdateProject(existingProject);
                    projectsDal.CommitChanges(); // Commit changes to the database
                    return existingProject; // Return the updated project
                }

                throw new Exception("Project not found"); // Throw an exception if project not found
            }
            catch (Exception ex)
            {
                return ex;
            }
        }




    }
}
