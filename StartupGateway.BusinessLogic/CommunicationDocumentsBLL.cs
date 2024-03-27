/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class ComsDocumentsBLL created.
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
    /// Business logic layer for managing operations related to model <see cref="CommunicationDocuments"/>.
    /// </summary>
    public class CommunicationDocumentsBLL
    {
        private readonly ILogger<ChatDetailsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize CommunicationDocumentsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public CommunicationDocumentsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>CommunicationDocuments</c> instance information for the Id passed.
        /// </summary>
        /// <param name="communicationDocumentId"></param>
        /// <returns>Instance of <see cref="CommunicationDocuments"/></returns>
        public CommunicationDocuments GetCommunicationDocumentById(int communicationDocumentId)
        {
            try
            {
                // Garbage Collection
                CommunicationDocuments? communicationDocuments;
                using (ICommunicationDocumentsDAL communicationDocumentsDAL= _unitOfWork.GetDAL<ICommunicationDocumentsDAL>()) 
                {
                    communicationDocuments = communicationDocumentsDAL.GetEntityById(communicationDocumentId);
                }
                   
                if (communicationDocuments != null)
                {
                    _logger.LogInformation("CommunicationDocuments instance retrieved succesfully for communication document ID: {communicationDocumentId}, at GetCommunicationDocumentById.", communicationDocumentId);
                    return communicationDocuments;
                }
                else
                {
                    _logger.LogWarning("No CommunicationDocuments instance found for communication document ID: {communicationDocumentId}, at GetCommunicationDocumentById", communicationDocumentId);
                    throw new CustomException("CommunicationDocuments instance retrieved at GetCommunicationDocumentById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving CommunicationDocuments instance for communication document ID: {communicationDocumentId}, at GetCommunicationDocumentById: {errorMessage}", communicationDocumentId, exception);
                throw new CustomException("Exception Caught at GetCommunicationDocumentById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>CommunicationDocuments</c>.
        /// </summary>
        /// <returns>List of <see cref="CommunicationDocuments"/>? Instances</returns>
        public List<CommunicationDocuments>? GetAllCommunicationDocuments()
        {
            try
            {
                // Garbage Collection
                List<CommunicationDocuments> lisOfCommunicationsDocuments;
                using (ICommunicationDocumentsDAL communicationDocumentsDAL = _unitOfWork.GetDAL<ICommunicationDocumentsDAL>()) 
                {
                    lisOfCommunicationsDocuments=communicationDocumentsDAL.GetAllRecords().ToList();
                }
                if (lisOfCommunicationsDocuments != null)
                {
                    _logger.LogInformation("All CommunicationDocuments instances retrieved successfully at GetAllCommunicationDocuments.");
                }
                else
                {
                    _logger.LogInformation("No CommunicationDocuments instances found at GetAllCommunicationDocuments.");
                }
                return lisOfCommunicationsDocuments;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving BidDocuments instances at GetAllCommunicationDocuments: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllCommunicationDocuments: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>CommunicationDocuments</c> to the Database.
        /// </summary>
        /// <param name="communicationDocument">New Instance of CommunicationDocuments to be saved.</param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="BidDocuments"/> added successfully.</returns>
        public bool AddCommunicationDocument(CommunicationDocuments communicationDocument, int userId)
        {
            try
            {
                if (communicationDocument != null)
                {
                    communicationDocument.Status = EntityStatus.Active;
                    communicationDocument.ModifiedBy = userId;
                    communicationDocument.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (ICommunicationDocumentsDAL communicationDocumentsDAL = _unitOfWork.GetDAL<ICommunicationDocumentsDAL>()) 
                    {
                        communicationDocumentsDAL.AddEntity(communicationDocument);
                    }
                    _unitOfWork.Commit();
                    _logger.LogInformation("New CommunicationDocuments instance successfully added at AddCommunicationDocument.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New CommunicationDocuments instance is null. Failed to add new CommunicationDocuments instance at AddCommunicationDocument.");
                    throw new CustomException("New CommunicationDocuments instance is null. Failed to add new CommunicationDocuments instance at AddCommunicationDocument.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new CommunicationDocuments instance at AddCommunicationDocument: {errorMessage}", exception);
                throw new CustomException("Exception Caught at CommunicationDocuments: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>CommunicationDocuments</c>.
        /// </summary>
        /// <param name="updatedCommunicationDocument"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdateCommunicationDocument(CommunicationDocuments updatedCommunicationDocument, int userId)
        {
            try
            {
                if (updatedCommunicationDocument != null)
                {

                    // Garbage Collection
                    CommunicationDocuments? existingCommunicationDocuments;
                    using ICommunicationDocumentsDAL communicationDocumentsDAL = _unitOfWork.GetDAL<ICommunicationDocumentsDAL>();

                    existingCommunicationDocuments = communicationDocumentsDAL.GetEntityById(updatedCommunicationDocument.Id);

                    if (existingCommunicationDocuments != null)
                    {
                        existingCommunicationDocuments.Status = updatedCommunicationDocument.Status;
                        existingCommunicationDocuments.ModifiedBy = userId;
                        existingCommunicationDocuments.ModifiedOn = DateTime.Now;

                        communicationDocumentsDAL.UpdateEntity(existingCommunicationDocuments);
                        _unitOfWork.Commit();

                        _logger.LogInformation("CommunicationDocuments instance with ID: {communicationDocumentId} updated successfully at UpdateCommunicationDocument.", existingCommunicationDocuments.Id);
                        return true;
                    }
                    else 
                    {
                        _logger.LogError("No CommunicationDocuments instance found for bid document ID: {communicationDocumentId}, at UpdateCommunicationDocument", updatedCommunicationDocument.Id);
                        throw new CustomException($"Existing CommunicationDocuments instance retrieved for bid document ID: {updatedCommunicationDocument.Id} is null at UpdateCommunicationDocument.");
                    }
                }
                else
                {
                    _logger.LogWarning("Updated CommunicationDocuments instance passed is null. Failed to update CommunicationDocuments instance at UpdateCommunicationDocument.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating CommunicationDocuments instance at UpdateCommunicationDocument: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateCommunicationDocument: " + exception + ".", exception);
            }
        }


    }
}
