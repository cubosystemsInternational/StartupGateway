/**
 * Created by: Shuaib
 * Created on: 21/03/2024
 * Description: Business logic class UserContactUsBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Shared;
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
    /// Business logic layer for managing operations related to model UserContactUsBLL.
    /// </summary>
    public class UserContactUsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize UserContactUsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public UserContactUsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserContactUs instance information for the id passed.
        /// </summary>
        /// <param name="userContactUsId"></param>
        /// <returns>UserContactUs</returns>
        public UserContactUs GetUserContactUsById(int userContactUsId) 
        {
            try
            {
                var userContactUs = unitOfWork.GetDAL<IUserContactUsDAL>().GetEntityById(userContactUsId);
                if (userContactUs != null)
                {
                    logger.LogInformation("UserContactUs instance retrieved succesfully at GetUserContactUsById.");
                    return userContactUs;
                }
                else
                {
                    logger.LogInformation("UserContactUs instance retrieved at GetUserContactUsById is null.");
                    throw new CustomException("UserContactUs instance retrieved at GetUserContactUsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetUserContactUsById: " + exception + ".");
                throw new CustomException("Exception Caught at GetUserContactUsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieves all instances of UserContactUs.
        /// </summary>
        /// <returns>List of UserContactUs</returns>
        public List<UserContactUs> GetAllUserContactUs() 
        {
            try
            {
                var listOfUserContactUs = unitOfWork.GetDAL<IUserContactUsDAL>().GetAllRecords().ToList();
                if (listOfUserContactUs != null)
                {
                    logger.LogInformation("UserContactUs instances retrieved successfully at GetAllUserContactUs.");
                    return listOfUserContactUs;
                }
                else
                {
                    logger.LogInformation("Instances of UserContactUs retrieved is null at GetAllUserContactUs.");
                    throw new CustomException("Instances of UserContactUs retrieved is null at GetAllUserContactUs.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllUserContactUs: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllUserContactUs: " + exception + ".");
            }
        }

        /// <summary>
        /// Adds an instance of UserContactUs to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="userContactUs"></param>
        /// <returns>True or False</returns>
        public bool AddUserContactUs(UserContactUs userContactUs) 
        {
            try
            {
                if (userContactUs != null)
                {
                    userContactUs.Status = EntityStatus.Active;
                    userContactUs.ModifiedBy = userContactUs.UserId;
                    userContactUs.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserContactUsDAL>().AddEntity(userContactUs);
                    unitOfWork.Commit();
                    logger.LogInformation("UserContactUs instance successfully added at AddUserContactUs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserContactUs instance is null at AddUserContactUs.");
                    throw new CustomException("UserContactUs instance is null at AddUserContactUs.");
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddUserContactUs: " + exception + ".");
                throw new CustomException("Exception Caught at AddUserContactUs: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of UserContactUs. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newUserContactUs"></param>
        /// <returns>True or False</returns>
        public bool  UpdateUserContactUs (UserContactUs newUserContactUs) 
        {
            try
            {
                if (newUserContactUs != null)
                {
                    UserContactUs existingChatDetails = unitOfWork.GetDAL<IUserContactUsDAL>().GetEntityById(newUserContactUs.Id);

                    
                    existingChatDetails.Status = newUserContactUs.Status;
                    existingChatDetails.ModifiedBy = newUserContactUs.UserId;
                    existingChatDetails.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserContactUsDAL>().UpdateEntity(existingChatDetails);
                    unitOfWork.Commit();

                    logger.LogInformation("UserContactUs instance updated successfully at UpdateUserContactUs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserContactUs instance passed at UpdateUserContactUs are null.");
                    throw new CustomException("UserContactUs instance passed at UpdateUserContactUs are null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateUserContactUs: " + exception + ".");
                throw new CustomException("Exception Caught at UpdateUserContactUs: " + exception + ".");
            }
        }
    }
}
