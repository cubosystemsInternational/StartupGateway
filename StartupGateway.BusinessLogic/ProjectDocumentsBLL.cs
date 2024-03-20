/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class ProjectDocumentsBLL created.
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
    /// Business logic layer for managing operations related to model ProjectDocumentsBLL.
    /// </summary>
    public class ProjectDocumentsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize ProjectDocumentsBLL with necessary dependencies.
        /// </summary>
        ///  <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public ProjectDocumentsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the ProjectDocuments instance information for the id passed.
        /// </summary>
        /// <param name="projectDocumentId"></param>
        /// <returns>ProjectDocuments?</returns>
        public ProjectDocuments? GetProjectDocumentsById(int projectDocumentId)
        {
            try
            {
                var projectDocuments = unitOfWork.GetDAL<IProjectDocumentsDAL>().GetEntityById(projectDocumentId);
                if (projectDocuments != null)
                {
                    logger.LogInformation("ProjectDocuments instance retrieved succesfully at GetProjectDocumentsById.");
                    return projectDocuments;
                }
                else
                {
                    logger.LogInformation("ProjectDocuments instance retrieved at GetProjectDocumentsById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetProjectDocumentsById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all instances of ProjectDocuments.
        /// </summary>
        /// <returns>List of ProjectDocuments?</returns>
        public List<ProjectDocuments>? GetAllProjectDocuments()
        {
            try
            {
                var listOfProjectDocuments = unitOfWork.GetDAL<IProjectDocumentsDAL>().GetAllRecords().ToList();
                logger.LogInformation("ProjectDocuments instances retrieved successfully at GetAllProjectDocuments.");
                return listOfProjectDocuments;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllProjectDocuments: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of ProjectDocuments to the Database. Returns True if operation was successful.
        /// </summary>
        /// <param name="projectDocument"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddProjectDocument(ProjectDocuments projectDocument, int userId) 
        {
            try
            {
                if (projectDocument != null)
                {
                    projectDocument.Status = EntityStatus.Active;
                    projectDocument.ModifiedBy = userId;
                    projectDocument.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IProjectDocumentsDAL>().AddEntity(projectDocument);
                    unitOfWork.Commit();
                    logger.LogInformation("ProjectDocuments instance successfully added at AddProjectDocument.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ProjectDocuments instance is null at AddProjectDocument");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddProjectDocument: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of ProjectDocuments. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newProjectDocument"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool UpdateProjectDocument(ProjectDocuments newProjectDocument,int userId) 
        {
            try
            {
                if (newProjectDocument != null)
                {
                    ProjectDocuments existingProjectDocument = unitOfWork.GetDAL<IProjectDocumentsDAL>().GetEntityById(newProjectDocument.ProjectDocumentId);


                    existingProjectDocument.AccessRights = newProjectDocument.AccessRights;
                    existingProjectDocument.Description= newProjectDocument.Description;

                    existingProjectDocument.Status = newProjectDocument.Status;
                    existingProjectDocument.ModifiedOn = DateTime.Now;
                    existingProjectDocument.ModifiedBy = userId;

                    unitOfWork.GetDAL<IProjectDocumentsDAL>().UpdateEntity(existingProjectDocument);
                    unitOfWork.Commit();

                    logger.LogInformation("ProjectDocuments instance updated successfully at UpdateProjectDocument.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ProjectDocuments instance passed at UpdateProjectDocument are null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateProjectDocument: " + exception + ".");
                return false;
            }
        }
    }
}
