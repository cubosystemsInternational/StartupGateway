/**
 * Created by: Ibrahim
 * Created on: 21/03/2024
 * Description: Business logic class for BidBLL .
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Shared;
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
    /// Business logic layer for managing operations related to model <see cref="Bids"/>.
    /// </summary>
    public class BidsBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BidsBLL> _logger;

        /// <summary>
        /// Constructor to initialize BidsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public BidsBLL(IUnitOfWork unitOfWork, ILogger<BidsBLL> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the bid details for the specified bid ID.
        /// </summary>
        /// <param name="bidsId">The ID of the bids instance to retrieve.</param>
        /// <returns>Instance of <see cref="Bids"/></returns>
        public Bids GetBidsById(int bidsId)
        {
            try
            {
                // Garbage Collection
                Bids? bids;
                using (IBidsDAL bidsDAL = _unitOfWork.GetDAL<IBidsDAL>())
                {
                    bids = bidsDAL.GetEntityById(bidsId);
                };

                if (bids != null)
                {
                    _logger.LogInformation("Bids instance retrieved succesfully for bid ID: {bidsId}, at GetBidsById.", bidsId);
                    return bids;
                }
                else
                {
                    _logger.LogWarning("No Bids instance found for bid ID: {bidDocumentId}, at GetBidsById", bidsId);
                    throw new CustomException("Bids instance retrieved at GetBidsById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Bids instance for bid ID: {bidId}, at GetBidsById: {errorMessage}",bidsId, exception);
                throw new CustomException("Exception Caught at GetBidsById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>Bids</c>.
        /// </summary>
        /// <returns>List of <see cref="Bids"/> Instances</returns>
        public List<Bids>? GetAllBids()
        {
            try
            {
                // Garbage Collections
                List<Bids> listOfBids;
                using (IBidsDAL bidsDAL = _unitOfWork.GetDAL<IBidsDAL>())
                {
                    listOfBids = bidsDAL.GetAllRecords().ToList();
                }

                if (listOfBids != null)
                {
                    _logger.LogInformation("All Bids instances retrieved successfully at GetAllBids.");
                }
                else 
                {
                    _logger.LogInformation("No Bids instances found at GetAllBids.");
                }
                return listOfBids;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Bids instances at GetAllBids: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllBids: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds a new bid.
        /// </summary>
        /// <param name="bids">The bid to add.</param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of Bids added successfully.</returns>
        public bool AddBids(Bids bids, int userId)
        {
            try
            {
                if (bids != null)
                {
                    bids.Status = EntityStatus.Active;
                    bids.ModifiedBy = userId;
                    bids.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (IBidsDAL bidsDAL = _unitOfWork.GetDAL<IBidsDAL>())
                    {
                        bidsDAL.AddEntity(bids);
                    }

                    _unitOfWork.Commit();
                    _logger.LogInformation("New Bids instance successfully added at AddBids.");
                    return true;
                }
                else 
                {
                    _logger.LogWarning("New Bids instance is null. Failed to add new Bids instance at AddBids.");
                    throw new CustomException("New Bids instance is null. Failed to add new Bids instance at AddBids.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new Bids instance at AddBids: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddBids: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing bid.
        /// </summary>
        /// <param name="updatedBids">The updated bid information.</param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public object UpdateBids(Bids updatedBids, int userId)
        {
            try
            {
                if (updatedBids != null)
                {
                    // Garbage Collection
                    Bids? existingBids;
                    using (IBidsDAL bidsDAL = _unitOfWork.GetDAL<IBidsDAL>())
                    {
                        existingBids = bidsDAL.GetEntityById(updatedBids.Id);
                    }

                    if (existingBids != null)
                    {
                        existingBids.Status = updatedBids.Status;
                        existingBids.ModifiedBy = updatedBids.ModifiedBy;
                        existingBids.ModifiedOn = DateTime.Now;

                        _unitOfWork.Commit();

                        _logger.LogInformation("Bids instance with ID: {bidsId} updated successfully at UpdateBids.", existingBids.Id);
                        return true;
                    }
                    else
                    {
                        _logger.LogError("No Bids instance found for bid ID: {bidsId}, at UpdateBids", updatedBids.Id);
                        throw new CustomException($"Existing Bids instance retrieved for bid ID: {updatedBids.Id} is null at UpdateBids is null.");
                    }

                }
                else 
                {
                    _logger.LogWarning("Updated Bids instance passed is null. Failed to update Bids instance at UpdateBids.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating Bids instance at UpdateBids: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateBids: " + exception + ".", exception);
            }
        }
    }
}
