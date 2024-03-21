/**
 * Created by: Ibrahim
 * Created on: 19/03/2024
 * Description: Business logic class for UserBLL .
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
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
    /// Business logic layer for managing operations related to model UserBLL.
    /// </summary>
    public class UserBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserBLL> _logger;

        /// <summary>
        /// Constructor to intialize UserBLL with necessary dependencies.
        /// </summary>
        public UserBLL(IUnitOfWork unitOfWork, ILogger<UserBLL> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the User Specific Detail information for the Id passed.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User</returns>
        public User GetUserById(int userId)
        {
            try
            {
                using var userRepository = _unitOfWork.GetDAL<IUserDAL>();
                return userRepository.GetEntityById(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user by ID: {UserId}", userId);
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        /// <summary>
        /// Retrieves The Specific User By Username
        /// </summary>
        /// <returns>User Details</returns>
        public User GetUserByUserName(string userName)
        {
            try
            {
                using var userRepository = _unitOfWork.GetDAL<IUserDAL>();
                return userRepository.GetEntityByAttribute(x => x.UserName == userName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user by name: {UserName}", userName);
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        /// <summary>
        /// Retrieves All The User's in a List
        /// </summary>
        /// <returns>List of User Details</returns>
        public List<User> GetAllUsers()
        {
            try
            {
                _logger.LogInformation("In GetAllUsers");
                using var userRepository = _unitOfWork.GetDAL<IUserDAL>();
                return (List<User>)userRepository.GetAllRecords();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all users.");
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                using var userRepository = _unitOfWork.GetDAL<IUserDAL>();
                user.Status = EntityStatus.Active;
                user.ModifiedOn = DateTime.Now;
                userRepository.AddEntity(user);
                _unitOfWork.Commit();
                _logger.LogInformation("User added successfully: {UserName}.", user.UserName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user: {UserName}.", user.UserName);
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        /// <summary>
        /// Updates an existing User.
        /// </summary>
        /// <param name="updatedUser">The updated User entity.</param>
        /// <returns>The updated User entity.</returns>
        public object UpdateUser(User updatedUser)
        {
            try
            {
                using var userRepository = _unitOfWork.GetDAL<IUserDAL>();
                var existingUser = userRepository.GetEntityById(updatedUser.UserId);

                if (existingUser != null)
                {
                    existingUser.UserName = updatedUser.UserName ?? existingUser.UserName;
                    existingUser.Email = updatedUser.Email ?? existingUser.Email;
                    existingUser.UserType = updatedUser.UserType;
                    existingUser.Status = updatedUser.Status;
                    existingUser.ModifiedBy = updatedUser.ModifiedBy;
                    existingUser.ModifiedOn = DateTime.Now; // Update modified date

                    userRepository.UpdateEntity(existingUser);
                    _unitOfWork.Commit();
                    return existingUser;
                }

                throw new Exception("User not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user with ID: {UserId}.", updatedUser.UserId);
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}
