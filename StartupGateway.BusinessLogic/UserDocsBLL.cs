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
    public class UserDocsBLL
    {
        private readonly ILogger<UserDocsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize UserDocsBLL with necessary dependencies.
        /// </summary>
        public UserDocsBLL(ILogger<UserDocsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserDocs information for the Id passed.
        /// </summary>
        /// <param name="userDocsId"></param>
        /// <returns>UserDocs?</returns>
        public UserDocs? GetUserDocsById(int userDocsId)
        {
            try
            {
                var userDocs = unitOfWork.GetDAL<IUserDocsDAL>().GetEntityById(userDocsId);
                if (userDocs != null)
                {
                    logger.LogInformation("UserDocs retrieved successfully at GetUserDocsById.");
                    return userDocs;
                }
                else
                {
                    logger.LogInformation("UserDocs retrieved at GetUserDocsById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetUserDocsById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the user documents.
        /// </summary>
        /// <returns>List of UserDocs?</returns>
        public List<UserDocs>? GetAllUserDocs()
        {
            try
            {
                var listOfUserDocs = unitOfWork.GetDAL<IUserDocsDAL>().GetAllRecords().ToList();
                logger.LogInformation("UserDocs retrieved successfully at GetAllUserDocs.");
                return listOfUserDocs;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllUserDocs: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of UserDocs to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="userDocs"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddUserDocs(UserDocs userDocs, int userId)
        {
            try
            {
                if (userDocs != null)
                {
                    userDocs.Status = EntityStatus.Active;
                    userDocs.ModifiedBy = userId;
                    userDocs.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserDocsDAL>().AddEntity(userDocs);
                    unitOfWork.Commit();
                    logger.LogInformation("UserDocs successfully added at AddUserDocs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserDocs is null at AddUserDocs");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddUserDocs: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of UserDocs. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserDocs"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserDocs(UserDocs newUserDocs)
        {
            try
            {
                if (newUserDocs != null)
                {
                    UserDocs existingUserDocs = unitOfWork.GetDAL<IUserDocsDAL>().GetEntityById(newUserDocs.UserDocsId);

                    // Update attributes if new values are not null or whitespace
                    existingUserDocs.Status = newUserDocs.Status != EntityStatus.Pending ? newUserDocs.Status : existingUserDocs.Status;
                    existingUserDocs.ModifiedOn = DateTime.Now;
                    existingUserDocs.ModifiedBy = newUserDocs.ModifiedBy != 0 ? newUserDocs.ModifiedBy : existingUserDocs.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("UserDocs updated successfully at UpdateUserDocs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserDocs passed at UpdateUserDocs is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateUserDocs: " + exception + ".");
                return false;
            }
        }
    }
}
