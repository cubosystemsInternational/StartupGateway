using System;
using Microsoft.Extensions.Logging;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;
using StartupGateway.UoW;
using StartupGateway.UoW.Interfaces;
using System.Collections.Generic;
using System.Linq;
using StartupGateway.BusinessEntities;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model UserDocsBLL.
    /// </summary>
    public class UserDocumentsBLL
    {
        private readonly ILogger<UserDocumentsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize UserDocumentsBLL with necessary dependencies.
        /// </summary>
        public UserDocumentsBLL(ILogger<UserDocumentsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserDocuments information for the Id passed.
        /// </summary>
        /// <param name="userDocumentsId"></param>
        /// <returns>UserDocs?</returns>
        public UserDocuments? GetUserDocumentsById(int userDocumentsId)
        {
            try
            {
                var userDocuments = unitOfWork.GetDAL<IUserDocumentsDAL>().GetEntityById(userDocumentsId);
                if (userDocuments != null)
                {
                    logger.LogInformation($"UserDocs with ID {userDocumentsId} retrieved successfully.");
                    return userDocuments;
                }
                else
                {
                    // Log the absence of the UserDocs and throw a KeyNotFoundException
                    var message = $"UserDocs with ID {userDocumentsId} not found.";
                    logger.LogError(message);
                    throw new KeyNotFoundException(message);
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetUserDocsById: " + exception + ".");
                throw;
            }
        }

        /// <summary>
        /// Retrieves all the UserDocuments instances.
        /// </summary>
        /// <returns>List of UserDocs?</returns>
        public List<UserDocuments>? GetAllUserDocuments()
        {
            try
            {
                var listOfUserDocuments = unitOfWork.GetDAL<IUserDocumentsDAL>().GetAllRecords().ToList();
                logger.LogInformation("UserDocuments retrieved successfully at GetAllUserDocuments.");
                return listOfUserDocuments;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllUserDocuments: " + exception + ".");
                throw;
            }
        }

        /// <summary>
        /// Adds an instance of UserDocuments to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="userDocuments"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddUserDocuments(UserDocuments userDocuments, int userId)
        {
            try
            {
                if (userDocuments != null)
                {
                    userDocuments.Status = EntityStatus.Active;
                    userDocuments.ModifiedBy = userId;
                    userDocuments.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserDocumentsDAL>().AddEntity(userDocuments);
                    unitOfWork.Commit();
                    logger.LogInformation("UserDocs successfully added at AddUserDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserDocs is null at AddUserDocuments.");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddUserDocuments: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of UserDocuments. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserDocuments"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserDocuments(UserDocuments newUserDocuments)
        {
            try
            {
                if (newUserDocuments != null)
                {
                    UserDocuments existingUserDocs = unitOfWork.GetDAL<IUserDocumentsDAL>().GetEntityById(newUserDocuments.Id);

                    // Update attributes if new values are not null or whitespace
                    existingUserDocs.Status = newUserDocuments.Status != EntityStatus.Pending ? newUserDocuments.Status : existingUserDocs.Status;
                    existingUserDocs.ModifiedOn = DateTime.Now;
                    existingUserDocs.ModifiedBy = newUserDocuments.ModifiedBy != 0 ? newUserDocuments.ModifiedBy : existingUserDocs.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("UserDocs updated successfully at UpdateUserDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserDocs passed at UpdateUserDocuments is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateUserDocuments: " + exception + ".");
                return false;
            }
        }
    }
}
