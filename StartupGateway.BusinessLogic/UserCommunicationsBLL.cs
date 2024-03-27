/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class UserCommunicationsBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Shared;
using StartupGateway.UoW.Interfaces;
using System;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model <see cref="UserCommunications"/>.
    /// </summary>
    public class UserCommunicationsBLL
    {
        private readonly ILogger<ChatDetailsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize UserCommunicationsBLL with necessary dependencies.
        /// </summary>
        /// <param name="_logger">Instance of Logger for logging information.</param>
        /// <param name="_unitOfWork">Instance of Unit of Work.</param>
        public UserCommunicationsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>UserCommunications</c> instance information for the Id passed.
        /// </summary>
        /// <param name="UserCommunications"></param>
        /// <returns>Instance of <see cref="UserCommunications"/></returns>
        public UserCommunications GetUserCommunicationsById(int userCommunicationsId)
        {
            try
            {

                // Garbage Collection 
                UserCommunications? userCommunications;
                using(IUserCommunicationsDAL userCommunicationsDAL = _unitOfWork.GetDAL<IUserCommunicationsDAL>())
                {
                    userCommunications = userCommunicationsDAL.GetEntityById(userCommunicationsId);
                }


                if (userCommunications != null)
                {
                    _logger.LogInformation("UserCommunications instance retrieved succesfully for user communications ID: {userCommunicationsId} , at GetUserCommunicationsById.", userCommunicationsId);
                    return userCommunications;
                }
                else
                {
                    _logger.LogInformation("No UserCommunications instances found for user communications ID: {userCommunicationsId}, at GetUserCommunicationsById", userCommunicationsId);
                    throw new CustomException("UserCommunications instance retrieved at GetUserCommunicationsById is null.");

                    
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving UserCommunications instance for user communications ID: {userCommunicationsId}, at GetUserCommunicationsById: {errorMessage}", userCommunicationsId, exception);
                throw new CustomException("Exception Caught at GetBidDocumentById: " + exception + ".", exception);


            }
        }

        /// <summary>
        /// Retrieves all instances of <c>UserCommunications</c>.
        /// </summary>
        /// <returns>List of <see cref="UserCommunications"/>? Instances</returns>
        public List<UserCommunications>? GetAllUserCommunnications()
        {
            try
            {
                List<UserCommunications> listOfUserCommunications;
                using (IUserCommunicationsDAL userCommunicationsDAL = _unitOfWork.GetDAL<IUserCommunicationsDAL>())
                {
                    listOfUserCommunications = userCommunicationsDAL.GetAllRecords().ToList();
                }
                if (listOfUserCommunications != null)
                {
                    _logger.LogInformation("All UserCommunications instances retrieved successfully at GetAllUserCommunnications.");
                }
                else
                {
                    _logger.LogInformation("No UserCommunications Instance found at GetAllUserCommunnications.");
                  
                }
                return listOfUserCommunications;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving UserCommunications instances at GetAllCommunications: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllCommunications: " + exception + ".", exception);

            }
        }

        /// <summary>
        /// Adds an instance of <c>UserCommunications</c>. to the Database. 
        /// </summary>
        /// <param name="UserCommunications"></param>
        /// <returns>True if new instance of <see cref="UserCommunications"/> added successfully.</returns>
        public bool AddUserCommunications(UserCommunications userCommunications)
        {
            try
            {
                if (userCommunications != null)
                {
                    userCommunications.Status = EntityStatus.Active;
                    userCommunications.ModifiedBy = userCommunications.UserId;
                    userCommunications.ModifiedOn = DateTime.Now;

                    using(IUserCommunicationsDAL userCommunicationsDAL = _unitOfWork.GetDAL<IUserCommunicationsDAL>())
                    {
                        userCommunicationsDAL.AddEntity(userCommunications);
                    }

                    _unitOfWork.Commit();
                    _logger.LogInformation("New UserCommunications instance successfully added at AddUserCommunications.");
                    return true;
                }
                else
                {

                    _logger.LogWarning("New UserCommunications instance is null. Failed to add new UserCommunications instance at AddUserCommunications.");
                    throw new CustomException("New UserCommunications instance is null. Failed to add new UserCommunications instance at AddUserCommunications.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new UserCommunications instance at AddUserCommunications: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddUserCommunications: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of UserCommunications. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserCommunications"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserCommunications(UserCommunications updatedUserCommunications, int userId)
        {
            try
            {

                if (updatedUserCommunications != null)
                {

                    using IUserCommunicationsDAL userCommunicationsDAL = _unitOfWork.GetDAL<IUserCommunicationsDAL>();
                    UserCommunications? existingUserCommunications = userCommunicationsDAL.GetEntityById(updatedUserCommunications.Id);

                    if(existingUserCommunications != null)
                    {
                        existingUserCommunications.Status = updatedUserCommunications.Status;
                        existingUserCommunications.ModifiedBy = userId;
                        existingUserCommunications.ModifiedOn = DateTime.Now;

                        _unitOfWork.Commit();
                        _logger.LogInformation("userCommunications instance with ID: {userCommunicationsId} updated successfully at UpdatedUserCommunications.", existingUserCommunications.Id);
                        return true;
                    }
                    else
                    {
                        _logger.LogError("No userCommunications instance found for bid document ID: {userCommunications}, at UpdatedUserCommunications", updatedUserCommunications.Id);
                        throw new CustomException($"Existing userCommunications instance retrieved for bid document ID: {updatedUserCommunications.Id} is null at UpdatedUserCommunications.");

                    }
                }
                else
                {
                    _logger.LogWarning("Updated userCommunications instance passed is null. Failed to update userCommunications instance at UpdatedUserCommunications.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating userCommunications instance at UpdatedUserCommunications: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdatedUserCommunications: " + exception + ".", exception);
            }
        }
    }
}
