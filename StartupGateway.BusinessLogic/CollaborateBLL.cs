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
        public Collaborate? GetCollaborateById(int comId)
        {
            try
            {
                var collaborate = unitOfWork.GetRepository<ICollaborateDAL>().GetEntityById(comId);
                if (collaborate != null)
                {
                    logger.LogInformation("Collaborate retrieved successfully at GetCollaborateById.");
                    return collaborate;
                }
                else
                {
                    logger.LogInformation("Collaborate retrieved at GetCollaborateById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetCollaborateById: " + exception + ".");
                return null;
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
                var listOfCollaborates = unitOfWork.GetRepository<ICollaborateDAL>().GetAllRecords().ToList();
                logger.LogInformation("Collaborates retrieved successfully at GetAllCollaborates.");
                return listOfCollaborates;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllCollaborates: " + exception + ".");
                return null;
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

                    unitOfWork.GetRepository<ICollaborateDAL>().AddEntity(collaborate);
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
                    Collaborate existingCollaborate = unitOfWork.GetRepository<ICollaborateDAL>().GetEntityById(newCollaborate.ComId);

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
