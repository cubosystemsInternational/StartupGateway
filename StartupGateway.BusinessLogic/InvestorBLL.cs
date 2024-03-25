/**
 * Created by: Ibrahim
 * Created on: 21/03/2024
 * Description: Business logic class for InvestorBLL .
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Model;
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
    /// Business logic layer for managing operations related to the Investor entity.
    /// </summary>
    public class InvestorBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<InvestorBLL> _logger;

        /// <summary>
        /// Constructor to initialize InvestorBLL with necessary dependencies.
        /// </summary>
        public InvestorBLL(IUnitOfWork unitOfWork, ILogger<InvestorBLL> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the investor details for the specified investor ID.
        /// </summary>
        /// <param name="investorId">The ID of the investor to retrieve.</param>
        /// <returns>Investor details.</returns>
        public Investor GetInvestorById(int investorId)
        {
            try
            {
                using var investorRepository = _unitOfWork.GetDAL<IInvestorDAL>();
                return investorRepository.GetEntityById(investorId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving investor by ID: {InvestorId}", investorId);
                throw;
            }
        }

        /// <summary>
        /// Retrieves all investors.
        /// </summary>
        /// <returns>List of investor details.</returns>
        public List<Investor> GetAllInvestors()
        {
            try
            {
                _logger.LogInformation("In GetAllInvestors");
                using var investorRepository = _unitOfWork.GetDAL<IInvestorDAL>();
                return investorRepository.GetAllRecords().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all investors.");
                throw;
            }
        }

        /// <summary>
        /// Adds a new investor.
        /// </summary>
        /// <param name="investor">The investor to add.</param>
        /// <returns>True if the investor was added successfully, otherwise false.</returns>
        public bool AddInvestor(Investor investor)
        {
            try
            {
                using var investorRepository = _unitOfWork.GetDAL<IInvestorDAL>();
                investor.Status = EntityStatus.Active;
                investor.ModifiedOn = DateTime.Now;
                investorRepository.AddEntity(investor);
                _unitOfWork.Commit();
                _logger.LogInformation("Investor added successfully: {InvestorId}.", investor.InvestorsId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding investor: {InvestorId}.", investor.InvestorsId);
                throw;
            }
        }

        /// <summary>
        /// Updates an existing investor.
        /// </summary>
        /// <param name="updatedInvestor">The updated investor information.</param>
        /// <returns>The updated investor details.</returns>
        public object UpdateInvestor(Investor updatedInvestor)
        {
            try
            {
                using var investorRepository = _unitOfWork.GetDAL<IInvestorDAL>();
                var existingInvestor = investorRepository.GetEntityById(updatedInvestor.InvestorsId);

                if (existingInvestor != null)
                {
                    existingInvestor.UserId = updatedInvestor.UserId;
                    existingInvestor.InvestmentValue = updatedInvestor.InvestmentValue;
                    existingInvestor.Status = updatedInvestor.Status;
                    existingInvestor.ModifiedBy = updatedInvestor.ModifiedBy;
                    existingInvestor.ModifiedOn = DateTime.Now; // Update modified date

                    investorRepository.UpdateEntity(existingInvestor);
                    _unitOfWork.Commit();
                    _logger.LogInformation("Investor Updated successfully: {InvestorId}.", existingInvestor.InvestorsId);
                    return existingInvestor;
                }

                throw new Exception("Investor not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating investor with ID: {InvestorId}.", updatedInvestor.InvestorsId);
                throw;
            }
        }
    }
}
