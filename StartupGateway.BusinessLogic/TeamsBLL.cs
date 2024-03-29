﻿/**
 * Modified by: Zaid
 * Created on: 03/21/2024
 * Description: Business logic class TeamsBLL is Modified.
 * 
 * */
using System;
using Microsoft.Extensions.Logging;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;
using StartupGateway.UoW;
using StartupGateway.UoW.Interfaces;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
	public class TeamsBLL
	{
        private readonly ILogger<TeamsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize TeamsBLL with necessary dependencies.
        /// </summary>
        public TeamsBLL(ILogger<TeamsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Team information for the Id passed.
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns>Team?</returns>
        public Team GetTeamById(int teamId)
        {
            try
            {
                var team = unitOfWork.GetDAL<ITeamDAL>().GetEntityById(teamId);
                if (team != null)
                {
                    logger.LogInformation("Team retrieved successfully at GetTeamById.");
                    return team;
                }
                else
                {
                    // Since the team is not found, log the information and throw an exception.
                    var errorMessage = $"Team with ID {teamId} not found.";
                    logger.LogInformation(errorMessage);
                    throw new KeyNotFoundException(errorMessage);
                }
            }
            catch (Exception exception)
            {
                // Log and rethrow the exception to be handled or logged by the calling code.
                logger.LogError($"Exception caught at GetTeamById: {exception}.");
                throw;
            }
        }


        /// <summary>
        /// Retrieves all the teams.
        /// </summary>
        /// <returns>List of Team?</returns>
        public List<Team> GetAllTeams()
        {
            try
            {
                var listOfTeams = unitOfWork.GetDAL<ITeamDAL>().GetAllRecords().ToList();

                if (listOfTeams == null || !listOfTeams.Any())
                {
                    // Log the absence of teams and throw an exception
                    logger.LogError("No teams found.");
                    throw new KeyNotFoundException("No teams found.");
                }

                logger.LogInformation("Teams retrieved successfully at GetAllTeams.");
                return listOfTeams;
            }
            catch (Exception exception)
            {
                // Log the exception and rethrow it
                logger.LogError($"Exception caught at GetAllTeams: {exception}.");
                throw;
            }
        }


        /// <summary>
        /// Adds an instance of Team to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="team"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddTeam(Team team, int userId)
        {
            try
            {
                if (team != null)
                {
                    team.Status = EntityStatus.Active;
                    team.ModifiedBy = userId;
                    team.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ITeamDAL>().AddEntity(team);
                    unitOfWork.Commit();
                    logger.LogInformation("Team successfully added at AddTeam.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Team is null at AddTeam");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddTeam: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of Team. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newTeam"></param>
        /// <returns>True or False</returns>
        public bool UpdateTeam(Team newTeam)
        {
            try
            {
                if (newTeam != null)
                {
                    Team existingTeam = unitOfWork.GetDAL<ITeamDAL>().GetEntityById(newTeam.TeamId);
                    // Update attributes if new values are not null or whitespace
                    existingTeam.TeamOwner = !string.IsNullOrWhiteSpace(newTeam.TeamOwner) ? newTeam.TeamOwner : existingTeam.TeamOwner;
                    existingTeam.TeamName = !string.IsNullOrWhiteSpace(newTeam.TeamName) ? newTeam.TeamName : existingTeam.TeamName;
                    existingTeam.Status = newTeam.Status != EntityStatus.Pending ? newTeam.Status : existingTeam.Status;
                    existingTeam.ModifiedOn = DateTime.Now;
                    existingTeam.ModifiedBy = newTeam.ModifiedBy != 0 ? newTeam.ModifiedBy : existingTeam.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("Team updated successfully at UpdateTeam.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Team passed at UpdateTeam is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateTeam: " + exception + ".");
                return false;
            }
        }

    }
}

