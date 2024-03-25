/**
 * Modified by: Zaid
 * Created on: 03/21/2024
 * Description: Business logic class CollabarateBLL Modified.
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
    public class CollaborateBLL
    {
        private readonly ILogger<CollaborateBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize CollaborateBLL with necessary dependencies.
        /// </summary>
        public CollaborateBLL(ILogger<CollaborateBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Collaborate information for the Id passed.
        /// </summary>
        /// <param name="comId"></param>
        /// <returns>Collaborate?</returns>
        public Collaborate GetCollaborateById(int comId)
        {
            try
            {
                var collaborate = unitOfWork.GetDAL<ICollaborateDAL>().GetEntityById(comId);
                if (collaborate != null)
                {
                    logger.LogInformation($"Collaborate with ID {comId} retrieved successfully.");
                    return collaborate;
                }
                else
                {
                    // Log the absence of the Collaborate entity and throw a KeyNotFoundException
                    var message = $"Collaborate with ID {comId} not found.";
                    logger.LogError(message);
                    throw new KeyNotFoundException(message);
                }
            }
            catch (Exception exception)
            {
                // Log and rethrow any other exceptions that occur during execution
                logger.LogError($"Exception caught at GetCollaborateById: {exception}.");
                throw;
            }
        }


        /// <summary>
        /// Retrieves all the collaborates.
        /// </summary>
        /// <returns>List of Collaborate?</returns>
        public List<Collaborate>? GetAllCollaborates()
        {
            try
            {
                var listOfCollaborates = unitOfWork.GetDAL<ICollaborateDAL>().GetAllRecords().ToList();
                logger.LogInformation("Collaborates retrieved successfully at GetAllCollaborates.");
                return listOfCollaborates;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllCollaborates: " + exception + ".");
                throw;
            }
        }

        /// <summary>
        /// Adds an instance of Collaborate to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="collaborate"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddCollaborate(Collaborate collaborate, int userId)
        {
            try
            {
                if (collaborate != null)
                {
                    collaborate.Status = EntityStatus.Active;
                    collaborate.ModifiedBy = userId;
                    collaborate.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ICollaborateDAL>().AddEntity(collaborate);
                    unitOfWork.Commit();
                    logger.LogInformation("Collaborate successfully added at AddCollaborate.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Collaborate is null at AddCollaborate");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddCollaborate: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of Collaborate. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newCollaborate"></param>
        /// <returns>True or False</returns>
        public bool UpdateCollaborate(Collaborate newCollaborate)
        {
            try
            {
                if (newCollaborate != null)
                {
                    Collaborate existingCollaborate = unitOfWork.GetDAL<ICollaborateDAL>().GetEntityById(newCollaborate.ComId);

                    // Update attributes if new values are not null or whitespace
                    existingCollaborate.Status = newCollaborate.Status != EntityStatus.Pending ? newCollaborate.Status : existingCollaborate.Status;
                    existingCollaborate.ModifiedOn = DateTime.Now;
                    existingCollaborate.ModifiedBy = newCollaborate.ModifiedBy != 0 ? newCollaborate.ModifiedBy : existingCollaborate.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("Collaborate updated successfully at UpdateCollaborate.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Collaborate passed at UpdateCollaborate is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateCollaborate: " + exception + ".");
                return false;
            }
        }
    }
}
