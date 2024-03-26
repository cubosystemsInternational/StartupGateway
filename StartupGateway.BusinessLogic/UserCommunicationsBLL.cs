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
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model UserComsBLL.
    /// </summary>
    public class UserCommunicationsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize UserCommunicationsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public UserCommunicationsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserCommunications instance information for the id passed.
        /// </summary>
        /// <param name="userCommunicationsId"></param>
        /// <returns>UserComs?</returns>
        public UserCommunications GetUserCommunicationsById(int userCommunicationsId)
        {
            try
            {
                var userCommunications = unitOfWork.GetDAL<IUserCommunicationsDAL>().GetEntityById(userCommunicationsId);
                if (userCommunications != null)
                {
                    logger.LogInformation("UserCommunications instance retrieved succesfully at GetUserCommunicationsById.");
                    return userCommunications;
                }
                else
                {
                    logger.LogInformation("UserCommunications instance retrieved at GetUserCommunicationsById is null.");
                    throw new CustomException("UserCommunications instance retrieved at GetUserCommunicationsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetUserCommunicationsById: " + exception + ".");
                throw new CustomException("Exception Caught at GetUserCommunicationsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieves all instances of UserCommunications.
        /// </summary>
        /// <returns>List of UserComs?</returns>
        public List<UserCommunications> GetAllUserCommunnications()
        {
            try
            {
                var listOfUserCommunications = unitOfWork.GetDAL<IUserCommunicationsDAL>().GetAllRecords().ToList();

                if (listOfUserCommunications != null)
                {
                    logger.LogInformation("UserCommunications instances retrieved successfully at GetAllUserCommunnications.");
                    return listOfUserCommunications;
                }
                else
                {
                    logger.LogInformation("Instance of UserCommunications retrieved is null at GetAllUserCommunnications.");
                    throw new CustomException("Instance of UserCommunications retrieved is null at GetAllUserCommunnications.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllUserCommunnications: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllUserCommunnications: " + exception + ".");
            }
        }

        /// <summary>
        /// Adds an instance of UserCommunications to the Database. Returns True if operation was successful.
        /// </summary>
        /// <param name="userCommunications"></param>
        /// <returns>True or False</returns>
        public bool AddUserCommunications(UserCommunications userCommunications)
        {
            try
            {
                if (userCommunications != null)
                {
                    userCommunications.Status = EntityStatus.Active;
                    userCommunications.ModifiedBy = userCommunications.UserId;
                    userCommunications.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserCommunicationsDAL>().AddEntity(userCommunications);
                    unitOfWork.Commit();
                    logger.LogInformation("UserCommunications instance successfully added at AddUserCommunications.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserCommunications instance is null at AddUserCommunications.");
                    throw new CustomException("UserCommunications instance is null at AddUserCommunications.");
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddUserCommunications: " + exception + ".");
                throw new CustomException("Exception Caught at AddUserCommunications: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of UserCommunications. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserCommunications"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserCommunications(UserCommunications newUserCommunications)
        {
            try
            {
                if (newUserCommunications != null)
                {
                    UserCommunications existingUserCommunications = unitOfWork.GetDAL<IUserCommunicationsDAL>().GetEntityById(newUserCommunications.Id);


                    existingUserCommunications.Status = newUserCommunications.Status;
                    existingUserCommunications.ModifiedBy = newUserCommunications.UserId;
                    existingUserCommunications.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserCommunicationsDAL>().UpdateEntity(existingUserCommunications);
                    unitOfWork.Commit();

                    logger.LogInformation("UserCommunications instance updated successfully at UpdateUserCommunications.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserCommunications instance passed at UpdateUserCommunications are null.");
                    throw new CustomException("UserCommunications instance passed at UpdateUserCommunications are null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateUserCommunications: " + exception + ".");
                throw new CustomException("Exception Caught at UpdateUserCommunications: " + exception + ".");
            }
        }
    }
}
