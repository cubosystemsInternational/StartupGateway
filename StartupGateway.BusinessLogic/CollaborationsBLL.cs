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
using StartupGateway.Shared;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model <see cref="Collaborations"/>.
    /// </summary>
    public class CollaborationsBLL
    {
        private readonly ILogger<CollaborationsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to initialize CollaborateBLL with necessary dependencies.
        /// </summary>
        /// /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public CollaborationsBLL(ILogger<CollaborationsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>Collaborations</c> instance information for the Id passed.
        /// </summary>
        /// <param name="collaborationsId"></param>
        /// <returns>Instance of <see cref="Collaborations"/></returns>
        public Collaborations GetCollaborationById(int collaborationsId)
        {
            try
            {
                // Garbage Collection
                Collaborations? collaborations;
                using (ICollaborationsDAL collaborationsDAL= _unitOfWork.GetDAL<ICollaborationsDAL>()) 
                {
                    collaborations=collaborationsDAL.GetEntityById(collaborationsId);
                }

                    
                if (collaborations != null)
                {
                    _logger.LogInformation("Collaborations instance retrieved succesfully for collaborations ID: {collaborationsId}, at GetCollaborationById.", collaborationsId);
                    return collaborations;
                }
                else
                {
                    _logger.LogWarning("No Collaborations instance found for collaborations ID: {collaborations}, at GetCollaborationById", collaborationsId);
                    throw new CustomException("Collaborations instance retrieved at GetCollaborationById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Collaborations instance for collaborations ID: {bidDocumentId}, at GetCollaborationById: {errorMessage}", collaborationsId, exception);
                throw new CustomException("Exception Caught at GetCollaborationById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>Collaborations</c>.
        /// </summary>
        /// <returns>List of <see cref="Collaborations"/>? Instances</returns>
        public List<Collaborations>? GetAllCollaborations()
        {
            try
            {
                // Garbage Collections
                List<Collaborations> listOfCollaborations;
                using (ICollaborationsDAL collaborationsDAL = _unitOfWork.GetDAL<ICollaborationsDAL>())
                {
                    listOfCollaborations = collaborationsDAL.GetAllRecords().ToList();
                }

                if (listOfCollaborations != null)
                {
                    _logger.LogInformation("All Collaborations instances retrieved successfully at GetAllCollaborations.");
                }
                else
                {
                    _logger.LogInformation("No Collaborations instances found at GetAllCollaborations.");
                }
                return listOfCollaborations;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Collaborations instances at GetAllCollaborations: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllCollaborations: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>Collaborations</c> to the Database.
        /// </summary>
        /// <param name="collaboration"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="Collaborations"/> added successfully.</returns>
        public bool AddCollaboration(Collaborations collaboration, int userId)
        {
            try
            {
                if (collaboration != null)
                {
                    collaboration.Status = EntityStatus.Active;
                    collaboration.ModifiedBy = userId;
                    collaboration.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (ICollaborationsDAL collaborationsDAL= _unitOfWork.GetDAL<ICollaborationsDAL>()) 
                    {
                        collaborationsDAL.AddEntity(collaboration);
                    }
                    _unitOfWork.Commit();
                    _logger.LogInformation("New Collaborations instance successfully added at AddCollaboration.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New Collaborations instance is null. Failed to add new Collaborations instance at AddCollaboration.");
                    throw new CustomException("New Collaborations instance is null. Failed to add new Collaborations instance at AddCollaboration.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new Collaborations instance at AddCollaboration: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddCollaboration: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>Collaborations</c>.
        /// </summary>
        /// <param name="updatedCollaborations"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise</returns>
        public bool UpdateCollaboration(Collaborations updatedCollaborations, int userId)
        {
            try
            {
                if (updatedCollaborations != null)
                {
                    // Garbage Collection 
                    Collaborations? existingCollaboration;
                    using ICollaborationsDAL collaborationsDAL = _unitOfWork.GetDAL<ICollaborationsDAL>();

                    existingCollaboration = collaborationsDAL.GetEntityById(updatedCollaborations.Id);

                    if (existingCollaboration != null)
                    {
                        existingCollaboration.Status = updatedCollaborations.Status;
                        existingCollaboration.ModifiedBy = userId;
                        existingCollaboration.ModifiedOn = DateTime.Now;

                        collaborationsDAL.UpdateEntity(existingCollaboration);

                        _unitOfWork.Commit();

                        _logger.LogInformation("Collaborations instance with ID: {collaborationsId} updated successfully at UpdateCollaboration.", existingCollaboration.Id);
                        return true;
                    }
                    else 
                    {
                        _logger.LogError("No Collaborations instance found for collaboration ID: {collaborationsId}, at UpdateCollaboration", updatedCollaborations.Id);
                        throw new CustomException($"Existing Collaborations instance retrieved for collaboration ID: {updatedCollaborations.Id} is null at UpdateCollaboration.");
                    }
                }
                else 
                {
                    _logger.LogWarning("Updated Collaborations instance passed is null. Failed to update Collaborations instance at UpdateCollaboration.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating Collaborations instance at UpdateCollaboration: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateCollaboration: " + exception + ".", exception);
            }
        }
    }
}
