/**
 * Created by: Zuhri
 * Created on: 19/03/2024
 * Description: Business logic class BidDocumentsBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using Mysqlx;
using Mysqlx.Crud;
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
    /// Business logic layer for managing operations related to model <see cref="BidDocuments"/>.
    /// </summary>
    public class BidDocumentsBLL
    {
        private readonly ILogger<BidDocumentsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize BidDocumentsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public BidDocumentsBLL(ILogger<BidDocumentsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>BidDocuments</c> instance information for the Id passed.
        /// </summary>
        /// <param name="bidDocumentsId"></param>
        /// <returns>Instance of <see cref="BidDocuments"/></returns>
        public BidDocuments GetBidDocumentsById(int bidDocumentsId)
        {
            try
            {
                // Garbage Collection 
                BidDocuments? bidDocuments;
                using (IBidDocumentsDAL bidDocumentsDAL = _unitOfWork.GetDAL<IBidDocumentsDAL>()) 
                {
                    bidDocuments = bidDocumentsDAL.GetEntityById(bidDocumentsId);
                }
                    
                if (bidDocuments != null)
                {
                    _logger.LogInformation("BidDocuments instance retrieved succesfully for bid document ID: {bidDocumentId}, at GetBidDocumentsById.", bidDocumentsId);
                    return bidDocuments;
                }
                else
                {
                    _logger.LogWarning("No BidDocuments instance found for bid document ID: {bidDocumentId}, at GetBidDocumentsById", bidDocumentsId);
                    throw new CustomException("BidDocuments instance retrieved at GetBidDocumentsById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,"Error retrieving BidDocuments instance for bid document ID: {bidDocumentId}, at GetBidDocumentsById: {errorMessage}",bidDocumentsId,exception);
                throw new CustomException("Exception Caught at GetBidDocumentsById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>BidDocuments</c>.
        /// </summary>
        /// <returns>List of <see cref="BidDocuments"/> Instances</returns>
        public List<BidDocuments>? GetAllBidDocuments()
        {
            try
            {
                // Garbage Collections
                List<BidDocuments> listOfBidDocuments;
                using (IBidDocumentsDAL bidDocumentsDAL = _unitOfWork.GetDAL<IBidDocumentsDAL>()) 
                {
                    listOfBidDocuments = bidDocumentsDAL.GetAllRecords().ToList();
                }

                if (listOfBidDocuments != null)
                {
                    _logger.LogInformation("All BidDocuments instances retrieved successfully at GetAllBidDocuments.");
                }
                else
                {
                    _logger.LogInformation("No BidDocuments instances found at GetAllBidDocuments.");
                }
                return listOfBidDocuments;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving BidDocuments instances at GetAllBidDocuments: {errorMessage}",exception);
                throw new CustomException("Exception Caught at GetAllBidDocuments: " + exception + ".",exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>BidDocuments</c> to the Database. 
        /// </summary>
        /// <param name="bidDocuments"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of BidDocuments added successfully.</returns>
        public bool AddBidDocuments(BidDocuments bidDocuments, int userId)
        {
            try
            {
                if (bidDocuments != null)
                {

                    bidDocuments.Status = EntityStatus.Active;
                    bidDocuments.ModifiedBy = userId;
                    bidDocuments.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (IBidDocumentsDAL bidDocumentsDAL = _unitOfWork.GetDAL<IBidDocumentsDAL>()) 
                    {
                        bidDocumentsDAL.AddEntity(bidDocuments);
                    }
                    _unitOfWork.Commit();
                    _logger.LogInformation("New BidDocuments instance successfully added at AddBidDocuments.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New BidDocuments instance is null. Failed to add new BidDocuments instance at AddBidDocuments.");
                    throw new CustomException("New BidDocuments instance is null. Failed to add new BidDocuments instance at AddBidDocuments.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception,"Error adding new BidDocuments instance at AddBidDocuments: {errorMessage}",exception);
                throw new CustomException("Exception Caught at AddBidDocuments: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>BidDocuments</c>.
        /// </summary>
        /// <param name="updatedBidDocuments"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdateBidDocuments(BidDocuments updatedBidDocuments, int userId)
        {
            try
            {
                if (updatedBidDocuments != null)
                {
                    // Garbage Collection
                    BidDocuments? existingBidDocuments;
                    using (IBidDocumentsDAL bidDocumentsDAL = _unitOfWork.GetDAL<IBidDocumentsDAL>()) 
                    {
                        existingBidDocuments = bidDocumentsDAL.GetEntityById(updatedBidDocuments.Id);
                    }


                    if (existingBidDocuments != null)
                    {
                        existingBidDocuments.Status = updatedBidDocuments.Status;
                        existingBidDocuments.ModifiedBy = userId;
                        existingBidDocuments.ModifiedOn = DateTime.Now;

                        _unitOfWork.Commit();

                        _logger.LogInformation("BidDocuments instance with ID: {bidDocumentsId} updated successfully at UpdateBidDocuments.", existingBidDocuments.Id);
                        return true;
                    }
                    else 
                    {
                        _logger.LogError("No BidDocuments instance found for bid document ID: {bidDocumentId}, at UpdateBidDocuments", updatedBidDocuments.Id);
                        throw new CustomException($"Existing BidDocuments instance retrieved for bid document ID: {updatedBidDocuments.Id} is null at updatedBidDocuments is null.");
                    }
                    
                }
                else
                {
                    _logger.LogWarning("Updated BidDocuments instance passed is null. Failed to update BidDocuments instance at UpdateBidDocuments.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating BidDocuments instance at UpdateBidDocuments: {errorMessage}",exception);
                throw new CustomException("Exception caught at UpdateBidDocuments: " + exception + ".", exception);
            }
        }
    }
}
