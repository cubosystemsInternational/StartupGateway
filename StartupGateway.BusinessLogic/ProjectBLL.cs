/**
 * Created by: Zuhri
 * Created on: 19/03/2024
 * Description: Business logic class ProjectBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using Mysqlx;
using Mysqlx.Crud;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model ProjectBLL.
    /// </summary>
    public class ProjectBLL
    {
        private readonly ILogger<ProjectBLL> logger;
        private readonly UnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize ProjectBLL with necessary dependencies.
        /// </summary>
        public ProjectBLL(ILogger<ProjectBLL> logger, UnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Project information for the Id passed.
        /// </summary>
        /// <param name="chatDetailId"></param>
        /// <returns>Project?</returns>
        public Project? GetProjectById(int chatDetailId)
        {
            try
            {
                var Project = unitOfWork.GetDAL<ProjectDAL>().GetEntityById(chatDetailId);
                if (Project != null)
                {
                    logger.LogInformation("Project retrieved succesfully at GetProjectById.");
                    return Project;
                }
                else
                {
                    logger.LogInformation("Project retrieved at GetProjectById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetProjectById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the Project.
        /// </summary>
        /// <returns>List of Project?</returns>
        public List<Project>? GetAllProject()
        {
            try
            {
                var listOfProject = unitOfWork.GetDAL<ProjectDAL>().GetAllRecords().ToList();
                logger.LogInformation("Project retrieved successfully at GetAllProject.");
                return listOfProject;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllProject: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of Project to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="Project"></param>
        /// <returns>True or False</returns>
        public bool AddProject(Project Project) 
        {
            try
            {
                if (Project != null)
                {
                    unitOfWork.GetDAL<ProjectDAL>().AddEntity(Project);
                    unitOfWork.Commit();
                    logger.LogInformation("Project successfully added at AddProject.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Project is null at AddProject");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddProject: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of Project. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newProject"></param>
        /// <returns>True or False</returns>
        public bool UpdateProject(Project newProject, int userId)
        {
            try
            {
                if (newProject != null)
                {
                    Project existingProject = unitOfWork.GetDAL<ProjectDAL>().GetEntityById(newProject.ProjectId);
                    existingProject.ProjectId = newProject.ProjectId;
                    existingProject.ProjectName = newProject.ProjectName;
                    existingProject.Status = newProject.Status;
                    existingProject.ModifiedOn = DateTime.Now;
                    existingProject.ModifiedBy = userId;

                    unitOfWork.Commit();

                    logger.LogInformation("Project updated successfully at UpdateProject.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Project passed at UpdateProject are null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateProject: " + exception + ".");
                return false;
            }
        }
    }
}
