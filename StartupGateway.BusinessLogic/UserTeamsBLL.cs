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
    public class UserTeamsBLL
    {
        private readonly ILogger<UserTeamsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize UserTeamsBLL with necessary dependencies.
        /// </summary>
        public UserTeamsBLL(ILogger<UserTeamsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the UserTeam information for the Id passed.
        /// </summary>
        /// <param name="userTeamId"></param>
        /// <returns>UserTeam?</returns>
        public UserTeam? GetUserTeamById(int userTeamId)
        {
            try
            {
                var userTeam = unitOfWork.GetDAL<IUserTeamDAL>().GetEntityById(userTeamId);
                if (userTeam != null)
                {
                    logger.LogInformation("UserTeam retrieved successfully at GetUserTeamById.");
                    return userTeam;
                }
                else
                {
                    logger.LogInformation("UserTeam retrieved at GetUserTeamById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetUserTeamById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the user teams.
        /// </summary>
        /// <returns>List of UserTeam?</returns>
        public List<UserTeam>? GetAllUserTeams()
        {
            try
            {
                var listOfUserTeams = unitOfWork.GetDAL<IUserTeamDAL>().GetAllRecords().ToList();
                logger.LogInformation("UserTeams retrieved successfully at GetAllUserTeams.");
                return listOfUserTeams;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllUserTeams: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of UserTeam to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="userTeam"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddUserTeam(UserTeam userTeam, int userId)
        {
            try
            {
                if (userTeam != null)
                {
                    userTeam.Status = EntityStatus.Active;
                    userTeam.ModifiedBy = userId;
                    userTeam.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IUserTeamDAL>().AddEntity(userTeam);
                    unitOfWork.Commit();
                    logger.LogInformation("UserTeam successfully added at AddUserTeam.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserTeam is null at AddUserTeam");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddUserTeam: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of UserTeam. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newUserTeam"></param>
        /// <returns>True or False</returns>
        public bool UpdateUserTeam(UserTeam newUserTeam)
        {
            try
            {
                if (newUserTeam != null)
                {
                    UserTeam existingUserTeam = unitOfWork.GetDAL<IUserTeamDAL>().GetEntityById(newUserTeam.UserTeamId);

                    // Update attributes if new values are not null or whitespace
                    existingUserTeam.Status = newUserTeam.Status != EntityStatus.Pending ? newUserTeam.Status : existingUserTeam.Status;
                    existingUserTeam.ModifiedOn = DateTime.Now;
                    existingUserTeam.ModifiedBy = newUserTeam.ModifiedBy != 0 ? newUserTeam.ModifiedBy : existingUserTeam.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("UserTeam updated successfully at UpdateUserTeam.");
                    return true;
                }
                else
                {
                    logger.LogInformation("UserTeam passed at UpdateUserTeam is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateUserTeam: " + exception + ".");
                return false;
            }
        }
    }
}
