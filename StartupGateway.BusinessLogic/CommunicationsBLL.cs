/**
 * Created by: Shuaib
 * Created on: 21/03/2024
 * Description: Business logic class CommunicationsBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Shared;
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
    /// Business logic layer for managing operations related to model <see cref="Communications"/>.
    /// </summary>
    public class CommunicationsBLL
    {
        private readonly ILogger<CommunicationsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize CommunicationsBLL with necessary dependencies.
        /// <paramref name="logger"/>
        /// <paramref name="unitOfWork"/>
        /// </summary>
        public CommunicationsBLL(ILogger<CommunicationsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>Communications</c> instance information for the Id passed.
        /// </summary>
        /// <param name="communicationsId"></param>
        /// <returns>Instance of <see cref="Communications"/></returns>
        public Communications GetCommunicationById(int communicationsId) 
        {
            try
            {
                // Garbage Collection
                Communications? communications;
                using (ICommunicationsDAL communicationsDAL= _unitOfWork.GetDAL<ICommunicationsDAL>()) 
                {
                    communications=communicationsDAL.GetEntityById(communicationsId);
                }
                    
                if (communications != null)
                {
                    _logger.LogInformation("Communications instance retrieved succesfully for communication ID: {communicationsId}, at GetCommunicationById.", communicationsId);
                    return communications;
                }
                else
                {
                    _logger.LogWarning("No Communications instance found for communication ID: {communicationsId}, at GetCommunicationById", communicationsId);
                    throw new CustomException("Communications instance retrieved at GetCommunicationById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Communications instance for communication ID: {communicationsId}, at GetCommunicationById: {errorMessage}", communicationsId, exception);
                throw new CustomException("Exception Caught at GetCommunicationById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>Communications</c>.
        /// </summary>
        /// <returns>List of <see cref="Communications"/>? Instances</returns>
        public List<Communications>? GetAllCommunications() 
        {
            try
            {
                var listOfCommunications = _unitOfWork.GetDAL<ICommunicationsDAL>().GetAllRecords().ToList();
                if (listOfCommunications != null)
                {
                    _logger.LogInformation("Communications instances retrieved successfully at GetAllCommunications.");
                    return listOfCommunications;
                }
                else
                {
                    _logger.LogInformation("Instances of Communications retrieved is null at GetAllCommunications.");
                    throw new CustomException("Instances of Communications retrieved is null at GetAllCommunications.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogInformation("Exception Caught at GetAllCommunications: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllCommunications: " + exception + ".");
            }
        }



        /// <summary>
        /// Adds an instance of <c>Communications</c> to the Database. 
        /// </summary>
        /// <param name="communications"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="Communications"/> added successfully.</returns>
        public bool AddCommmunication(Communications communications, int userId) 
        {
            try
            {
                if (communications != null)
                {
                    communications.Status = EntityStatus.Active;
                    communications.ModifiedBy = userId;
                    communications.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (ICommunicationsDAL communicationsDAL = _unitOfWork.GetDAL<ICommunicationsDAL>())
                    {
                        communicationsDAL.AddEntity(communications);
                    }

                    _unitOfWork.Commit();
                    _logger.LogInformation("New Communications instance successfully added at AddCommmunication.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New Communications instance is null. Failed to add new Communications instance at AddCommmunication.");
                    throw new CustomException("New Communications instance is null. Failed to add new Communications instance at AddCommmunication.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new Communications instance at AddCommmunication: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddCommmunication: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>Communications</c>.
        /// </summary>
        /// <param name="updatedCommunications"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdateCommunication(Communications updatedCommunications, int userId) 
        {
            try
            {
                if (updatedCommunications != null)
                {
                    // Garbage Collection
                    Communications? existingCommunications;
                    using ICommunicationsDAL communicationsDAL = _unitOfWork.GetDAL<ICommunicationsDAL>();
                    existingCommunications = communicationsDAL.GetEntityById(updatedCommunications.Id);

                    if (existingCommunications != null)
                    {

                        existingCommunications.Subject = !string.IsNullOrWhiteSpace(updatedCommunications.Subject) ? updatedCommunications.Subject : existingCommunications.Subject;
                        existingCommunications.Message = !string.IsNullOrWhiteSpace(updatedCommunications.Message) ? updatedCommunications.Message : existingCommunications.Message;

                        existingCommunications.Status = updatedCommunications.Status;
                        existingCommunications.ModifiedBy = userId;
                        existingCommunications.ModifiedOn = DateTime.Now;

                        communicationsDAL.UpdateEntity(existingCommunications);

                        _unitOfWork.Commit();

                        _logger.LogInformation("Communications instance with ID: {communicationId} updated successfully at UpdateCommunication.", existingCommunications.Id);
                        return true;
                    }
                    else
                    {
                        _logger.LogError("No Communications instance found for communication ID: {communicationId}, at UpdateCommunication", updatedCommunications.Id);
                        throw new CustomException($"Existing Communications instance retrieved for communication ID: {updatedCommunications.Id} is null at UpdateCommunication.");
                    }
                }
                else
                {
                    _logger.LogWarning("Updated Communications instance passed is null. Failed to update Communications instance at UpdateCommunication.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating Communications instance at UpdateCommunication: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateCommunication: " + exception + ".", exception);
            }
        }
    }
}
