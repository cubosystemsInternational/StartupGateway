/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class UserComsBLL created.
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
    /// Business logic layer for managing operations related to model UserComsBLL.
    /// </summary>
    public class UserComsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize UserComsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public UserComsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserComs instance information for the id passed.
        /// </summary>
        /// <param name="userComsId"></param>
        /// <returns>UserComs?</returns>
        public UserComs? GetUserComsById(int userComsId)
        {
            try
            {
                var userComs = unitOfWork.GetDAL<IUserComsDAL>().GetEntityById(userComsId);
                if (userComs != null)
                {
                    logger.LogInformation("UserComs instance retrieved succesfully at GetUserComsById.");
                    return userComs;
                }
                else
                {
                    logger.LogInformation("UserComs instance retrieved at GetUserComsById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetUserComsById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all instances of UserComs.
        /// </summary>
        /// <returns>List of UserComs?</returns>
        public List<UserComs>? GetAllUserComs()
        {
            try
            {
                var listOfUserComs = unitOfWork.GetDAL<IUserComsDAL>().GetAllRecords().ToList();
                logger.LogInformation("UserComs instances retrieved successfully at GetAllUserComs.");
                return listOfUserComs;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllUserComs: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of UserComs to the Database. Returns True if operation was successful.
        /// </summary>
        /// <param name="userComs"></param>
        /// <returns>True or False</returns>
        public bool AddUserComs(UserComs userComs)
        {
            try
            {
                if (userComs != null)
                {
                    userComs.Status = EntityStatus.Active;
                    userComs.ModifiedBy = userComs.UserId;
                    userComs.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserComsDAL>().AddEntity(userComs);
                    unitOfWork.Commit();
                    logger.LogInformation("UserComs instance successfully added at AddUserComs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserComs instance is null at AddUserComs.");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddCompanyDocuments: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of UserComs. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserComs"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserComs(UserComs newUserComs)
        {
            try
            {
                if (newUserComs != null)
                {
                    UserComs existingUserCom = unitOfWork.GetDAL<IUserComsDAL>().GetEntityById(newUserComs.UserComId);

                    
                    existingUserCom.Status = newUserComs.Status;
                    existingUserCom.ModifiedOn = DateTime.Now;
                    existingUserCom.ModifiedBy = newUserComs.UserId;

                    unitOfWork.GetDAL<IUserComsDAL>().UpdateEntity(existingUserCom);
                    unitOfWork.Commit();

                    logger.LogInformation("UserComs instance updated successfully at UpdateUserComs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserComs instance passed at UpdateUserComs are null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateUserComs: " + exception + ".");
                return false;
            }
        }
    }
}
