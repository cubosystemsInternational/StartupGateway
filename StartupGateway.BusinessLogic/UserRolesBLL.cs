/**
 * Modified by: Zaid
 * Created on: 03/21/2024
 * Description: Business logic class UserRolesBLL Modified.
 * 
 * */
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
    public class UserRolesBLL
    {
        private readonly ILogger<UserRolesBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize UserRolesBLL with necessary dependencies.
        /// </summary>
        public UserRolesBLL(ILogger<UserRolesBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserRole information for the Id passed.
        /// </summary>
        /// <param name="userRoleId"></param>
        /// <returns>UserRole?</returns>
        public UserRoles GetUserRoleById(int userRoleId)
        {
            try
            {
                var userRole = unitOfWork.GetDAL<IUserRolesDAL>().GetEntityById(userRoleId);
                if (userRole != null)
                {
                    logger.LogInformation($"UserRole with ID {userRoleId} retrieved successfully.");
                    return userRole;
                }
                else
                {
                    // Log the absence of the UserRole and throw a KeyNotFoundException
                    var message = $"UserRole with ID {userRoleId} not found.";
                    logger.LogError(message);
                    throw new KeyNotFoundException(message);
                }
            }
            catch (Exception exception)
            {
                // Log and rethrow any other exceptions that occur during execution
                logger.LogError($"Exception caught at GetUserRoleById: {exception}.");
                throw;
            }
        }


        /// <summary>
        /// Retrieves all the user roles.
        /// </summary>
        /// <returns>List of UserRole?</returns>
        public List<UserRoles>? GetAllUserRoles()
        {
            try
            {
                var listOfUserRoles = unitOfWork.GetDAL<IUserRolesDAL>().GetAllRecords().ToList();
                logger.LogInformation("UserRoles retrieved successfully at GetAllUserRoles.");
                return listOfUserRoles;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllUserRoles: " + exception + ".");
                throw;
            }
        }

        /// <summary>
        /// Adds an instance of UserRole to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="userRole"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddUserRole(UserRoles userRole, int userId)
        {
            try
            {
                if (userRole != null)
                {
                    userRole.Status = EntityStatus.Active;
                    userRole.ModifiedBy = userId;
                    userRole.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserRolesDAL>().AddEntity(userRole);
                    unitOfWork.Commit();
                    logger.LogInformation("UserRole successfully added at AddUserRole.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserRole is null at AddUserRole");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddUserRole: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of UserRole. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserRole"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserRole(UserRoles newUserRole)
        {
            try
            {
                if (newUserRole != null)
                {
                    UserRoles existingUserRole = unitOfWork.GetDAL<IUserRolesDAL>().GetEntityById(newUserRole.Id);

                    // Update attributes if new values are not null or whitespace
                    existingUserRole.Status = newUserRole.Status != EntityStatus.Pending ? newUserRole.Status : existingUserRole.Status;
                    existingUserRole.ModifiedOn = DateTime.Now;
                    existingUserRole.ModifiedBy = newUserRole.ModifiedBy != 0 ? newUserRole.ModifiedBy : existingUserRole.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("UserRole updated successfully at UpdateUserRole.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserRole passed at UpdateUserRole is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateUserRole: " + exception + ".");
                return false;
            }
        }
    }
}
