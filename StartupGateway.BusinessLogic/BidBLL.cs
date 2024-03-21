/**
 * Created by: Ibrahim
 * Created on: 21/03/2024
 * Description: Business logic class for BidBLL .
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
    /// Business logic layer for managing operations related to the Bid entity.
    /// </summary>
    public class BidBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BidBLL> _logger;

        /// <summary>
        /// Constructor to initialize BidBLL with necessary dependencies.
        /// </summary>
        public BidBLL(IUnitOfWork unitOfWork, ILogger<BidBLL> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the bid details for the specified bid ID.
        /// </summary>
        /// <param name="bidId">The ID of the bid to retrieve.</param>
        /// <returns>Bid details.</returns>
        public Bid GetBidById(int bidId)
        {
            try
            {
                using var bidRepository = _unitOfWork.GetDAL<IBidDAL>();
                return bidRepository.GetEntityById(bidId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving bid by ID: {BidId}", bidId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves all bids.
        /// </summary>
        /// <returns>List of bid details.</returns>
        public List<Bid> GetAllBids()
        {
            try
            {
                _logger.LogInformation("In GetAllBids");
                using var bidRepository = _unitOfWork.GetDAL<IBidDAL>();
                return bidRepository.GetAllRecords().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all bids.");
                throw;
            }
        }

        /// <summary>
        /// Adds a new bid.
        /// </summary>
        /// <param name="bid">The bid to add.</param>
        /// <returns>True if the bid was added successfully, otherwise false.</returns>
        public bool AddBid(Bid bid)
        {
            try
            {
                using var bidRepository = _unitOfWork.GetDAL<IBidDAL>();
                bid.Status = EntityStatus.Active;
                bid.ModifiedOn = DateTime.Now;
                bidRepository.AddEntity(bid);
                _unitOfWork.Commit();
                _logger.LogInformation("Bid added successfully: {BidId}.", bid.BidId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding bid: {BidId}.", bid.BidId);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing bid.
        /// </summary>
        /// <param name="updatedBid">The updated bid information.</param>
        /// <returns>The updated bid details.</returns>
        public object UpdateBid(Bid updatedBid)
        {
            try
            {
                using var bidRepository = _unitOfWork.GetDAL<IBidDAL>();
                var existingBid = bidRepository.GetEntityById(updatedBid.BidId);

                if (existingBid != null)
                {
                    existingBid.UserId = updatedBid.UserId;
                    existingBid.ProjectID = updatedBid.ProjectID;
                    existingBid.InvestmentBudget = updatedBid.InvestmentBudget;
                    existingBid.Status = updatedBid.Status;
                    existingBid.ModifiedBy = updatedBid.ModifiedBy;
                    existingBid.ModifiedOn = DateTime.Now; // Update modified date

                    bidRepository.UpdateEntity(existingBid);
                    _unitOfWork.Commit();
                    return existingBid;
                }

                throw new Exception("Bid not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating bid with ID: {BidId}.", updatedBid.BidId);
                throw;
            }
        }
    }
}
