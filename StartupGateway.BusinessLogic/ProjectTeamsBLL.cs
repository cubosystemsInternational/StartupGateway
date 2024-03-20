/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class ProjectTeamsBLL created.
 * 
 * */


using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.UoW;
using StartupGateway.UoW.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model ProjectTeamsBLL.
    /// </summary>
    public class ProjectTeamsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize ProjectTeamsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public ProjectTeamsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the ProjectTeams instance information for the id passed.
        /// </summary>
        /// <param name="projectTeamsId"></param>
        /// <returns>ProjectTeams?</returns>
        public ProjectTeams? GetProjectTeamsById(int projectTeamsId)
        {
            try
            {
                var projectTeams = unitOfWork.GetDAL<IProjectTeamsDAL>().GetEntityById(projectTeamsId);
                if (projectTeams != null)
                {
                    logger.LogInformation("ProjectTeams instance retrieved succesfully at GetProjectTeamsById.");
                    return projectTeams;
                }
                else
                {
                    logger.LogInformation("ProjectTeams instance retrieved at GetProjectTeamsById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetProjectTeamsById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all instances of ProjectTeams.
        /// </summary>
        /// <returns>List of ProjectTeams?</returns>
        public List<ProjectTeams>? GetAllProjectTeams()
        {
            try
            {
                var listOfProjectTeams = unitOfWork.GetDAL<IProjectTeamsDAL>().GetAllRecords().ToList();
                logger.LogInformation("ProjectTeams instances retrieved successfully at GetAllProjectTeams.");
                return listOfProjectTeams;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllProjectTeams: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of ProjectTeams to the Database. Returns True if operation was successful.
        /// </summary>
        /// <param name="projectTeams"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddProjectTeams(ProjectTeams projectTeams, int userId)
        {
            try
            {
                if (projectTeams != null)
                {
                    projectTeams.Status = EntityStatus.Active;
                    projectTeams.ModifiedBy = userId;
                    projectTeams.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IProjectTeamsDAL>().AddEntity(projectTeams);
                    unitOfWork.Commit();
                    logger.LogInformation("ProjectTeams instance successfully added at AddProjectTeams.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ProjectTeams instance is null at AddProjectTeams");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddProjectTeams: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of ProjectTeams. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newProjectTeams"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool UpdateProjectTeams(ProjectTeams newProjectTeams, int userId)
        {
            try
            {
                if (newProjectTeams != null)
                {
                    ProjectTeams existingProjectTeam = unitOfWork.GetDAL<IProjectTeamsDAL>().GetEntityById(newProjectTeams.ProjectTeamsId);

                    existingProjectTeam.Status = newProjectTeams.Status;
                    existingProjectTeam.ModifiedOn = DateTime.Now;
                    existingProjectTeam.ModifiedBy = userId;

                    unitOfWork.GetDAL<IProjectTeamsDAL>().UpdateEntity(existingProjectTeam);
                    unitOfWork.Commit();

                    logger.LogInformation("ProjectTeams instance updated successfully at UpdateProjectTeams.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ProjectTeams instance passed at UpdateProjectTeams are null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateProjectTeams: " + exception + ".");
                return false;
            }
        }
    }
}
