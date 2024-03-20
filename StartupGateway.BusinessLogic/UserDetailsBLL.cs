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
    public class UserDetailsBLL
    {
        private readonly ILogger<UserDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize UserDetailsBLL with necessary dependencies.
        /// </summary>
        public UserDetailsBLL(ILogger<UserDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserDetails information for the Id passed.
        /// </summary>
        /// <param name="userDetailsId"></param>
        /// <returns>UserDetails?</returns>
        public UserDetails? GetUserDetailsById(int userDetailsId)
        {
            try
            {
                var userDetails = unitOfWork.GetRepository<IUserDetailsDAL>().GetEntityById(userDetailsId);
                if (userDetails != null)
                {
                    logger.LogInformation("UserDetails retrieved successfully at GetUserDetailsById.");
                    return userDetails;
                }
                else
                {
                    logger.LogInformation("UserDetails retrieved at GetUserDetailsById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetUserDetailsById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the user details.
        /// </summary>
        /// <returns>List of UserDetails?</returns>
        public List<UserDetails>? GetAllUserDetails()
        {
            try
            {
                var listOfUserDetails = unitOfWork.GetRepository<IUserDetailsDAL>().GetAllRecords().ToList();
                logger.LogInformation("UserDetails retrieved successfully at GetAllUserDetails.");
                return listOfUserDetails;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllUserDetails: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of UserDetails to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="userDetails"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddUserDetails(UserDetails userDetails, int userId)
        {
            try
            {
                if (userDetails != null)
                {
                    userDetails.Status = EntityStatus.Active;
                    userDetails.ModifiedBy = userId;
                    userDetails.ModifiedOn = DateTime.Now;

                    unitOfWork.GetRepository<IUserDetailsDAL>().AddEntity(userDetails);
                    unitOfWork.Commit();
                    logger.LogInformation("UserDetails successfully added at AddUserDetails.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserDetails is null at AddUserDetails");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddUserDetails: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of UserDetails. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserDetails"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserDetails(UserDetails newUserDetails)
        {
            try
            {
                if (newUserDetails != null)
                {
                    UserDetails existingUserDetails = unitOfWork.GetRepository<IUserDetailsDAL>().GetEntityById(newUserDetails.UserDetailsId);

                    // Update attributes if new values are not null or whitespace
                    existingUserDetails.FirstName = !string.IsNullOrWhiteSpace(newUserDetails.FirstName) ? newUserDetails.FirstName : existingUserDetails.FirstName;
                    existingUserDetails.LastName = !string.IsNullOrWhiteSpace(newUserDetails.LastName) ? newUserDetails.LastName : existingUserDetails.LastName;
                    existingUserDetails.Status = newUserDetails.Status != EntityStatus.Pending ? newUserDetails.Status : existingUserDetails.Status;
                    existingUserDetails.ModifiedOn = DateTime.Now;
                    existingUserDetails.ModifiedBy = newUserDetails.ModifiedBy != 0 ? newUserDetails.ModifiedBy : existingUserDetails.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("UserDetails updated successfully at UpdateUserDetails.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserDetails passed at UpdateUserDetails is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateUserDetails: " + exception + ".");
                return false;
            }
        }
    }
}
