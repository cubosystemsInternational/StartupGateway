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
using StartupGateway.Shared;
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
        public ProjectDocuments GetProjectDocumentsById(int projectDocumentId)
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
                    throw new CustomException("ProjectDocuments instance retrieved at GetProjectDocumentsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetProjectDocumentsById: " + exception + ".");
                throw new CustomException("Exception Caught at GetProjectDocumentsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieves all instances of ProjectDocuments.
        /// </summary>
        /// <returns>List of ProjectDocuments?</returns>
        public List<ProjectDocuments> GetAllProjectDocuments()
        {
            try
            {
                var listOfProjectDocuments = unitOfWork.GetDAL<IProjectDocumentsDAL>().GetAllRecords().ToList();
                if (listOfProjectDocuments != null)
                {
                    logger.LogInformation("ProjectDocuments instances retrieved successfully at GetAllProjectDocuments.");
                    return listOfProjectDocuments;

                }
                else 
                {
                    logger.LogInformation("Instances of ProjectDocuments retrieved is null at GetAllProjectDocuments.");
                    throw new CustomException("Instances of ProjectDocuments retrieved is null at GetAllProjectDocuments.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllProjectDocuments: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllProjectDocuments: " + exception + ".") ;
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
                    throw new CustomException("ProjectDocuments instance is null at AddProjectDocument");
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddProjectDocument: " + exception + ".");
                throw new CustomException("Exception Caught at AddProjectDocument: " + exception + ".");
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
                    ProjectDocuments existingProjectDocument = unitOfWork.GetDAL<IProjectDocumentsDAL>().GetEntityById(newProjectDocument.Id);


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
                    throw new CustomException("ProjectDocuments instance passed at UpdateProjectDocument are null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateProjectDocument: " + exception + ".");
                throw new Exception("Exception Caught at UpdateProjectDocument: " + exception + ".");
            }
        }
    }
}
