/**
 * Modified by: Zaid
 * Created on: 03/21/2024
 * Description: Business logic class CompaniesBLL Modified.
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
using StartupGateway.Shared;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model <see cref="Companies"/>.
    /// </summary>
    public class CompaniesBLL
    {
        private readonly ILogger<CompaniesBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to initialize CompaniesBLL with necessary dependencies.
        /// </summary>
        /// /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public CompaniesBLL(ILogger<CompaniesBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>Companies</c> instance information for the Id passed.
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>Instance of <see cref="Companies"/></returns>
        public Companies GetCompanyById(int companyId)
        {
            try
            {
                // Garbage Collection
                Companies? companies;
                using (ICompaniesDAL companiesDAL = _unitOfWork.GetDAL<ICompaniesDAL>()) 
                { 
                    companies = companiesDAL.GetEntityById(companyId);
                }

                if (companies != null)
                {
                    _logger.LogInformation("Companies instance retrieved succesfully for company ID: {companyId}, at GetCompanyById.", companyId);
                    return companies;
                }
                else
                {
                    _logger.LogWarning("No Companies instance found for company ID: {companyId}, at GetCompanyById", companyId);
                    throw new CustomException("Companies instance retrieved at GetCompanyById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Companies instance for company ID: {companyId}, at GetCompanyById: {errorMessage}", companyId, exception);
                throw new CustomException("Exception Caught at GetCompanyById: " + exception + ".", exception);
            }
        }


        /// <summary>
        /// Retrieves all instances of <c>Companies</c>.
        /// </summary>
        /// <returns>List of <see cref="Companies"/>? Instances</returns>
        public List<Companies>? GetAllCompanies()
        {
            try
            {
                // Garbage Collection
                List<Companies> listOfCompanies;
                using (ICompaniesDAL companiesDAL = _unitOfWork.GetDAL<ICompaniesDAL>()) 
                {
                    listOfCompanies = companiesDAL.GetAllRecords().ToList();
                }

                if (listOfCompanies != null)
                {
                    _logger.LogInformation("All Companies instances retrieved successfully at GetAllCompanies.");
                }
                else 
                {
                    _logger.LogInformation("No Companies instances found at GetAllCompanies.");
                }
                return listOfCompanies;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Companies instances at GetAllCompanies: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllCompanies: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>Companies</c> to the Database. 
        /// </summary>
        /// <param name="company"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="Companies"/> added successfully.</returns>
        public bool AddCompany(Companies company, int userId)
        {
            try
            {
                if (company != null)
                {
                    company.Status = EntityStatus.Active;
                    company.ModifiedBy = userId;
                    company.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (ICompaniesDAL companiesDAL = _unitOfWork.GetDAL<ICompaniesDAL>()) 
                    {
                        companiesDAL.AddEntity(company);
                    }

                    _unitOfWork.Commit();
                    _logger.LogInformation("New Companies instance successfully added at AddCompany.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New Companies instance is null. Failed to add new Companies instance at AddCompany.");
                    throw new CustomException("New Companies instance is null. Failed to add new Companies instance at AddCompany.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new Companies instance at AddCompany: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddCompany: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>Companies</c>.
        /// </summary>
        /// <param name="updatedCompany"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdateCompany(Companies updatedCompany,int userId)
        {
            try
            {
                if (updatedCompany != null)
                {
                    // Garbage Collection
                    Companies? existingCompany;
                    using ICompaniesDAL companiesDAL = _unitOfWork.GetDAL<ICompaniesDAL>();
                    existingCompany = companiesDAL.GetEntityById(updatedCompany.Id);

                    if (existingCompany != null)
                    {
                        existingCompany.CompanyName = !string.IsNullOrWhiteSpace(updatedCompany.CompanyName)? updatedCompany.CompanyName: existingCompany.CompanyName;
                        existingCompany.Description = !string.IsNullOrWhiteSpace(updatedCompany.Description) ? updatedCompany.Description : existingCompany.Description;

                        existingCompany.Status = updatedCompany.Status;
                        existingCompany.ModifiedBy = userId;
                        existingCompany.ModifiedOn = DateTime.Now;

                        companiesDAL.UpdateEntity(existingCompany);
                        _unitOfWork.Commit();

                        _logger.LogInformation("Companies instance with ID: {companyId} updated successfully at UpdateCompany.", existingCompany.Id);
                        return true;
                    }
                    else 
                    {
                        _logger.LogError("No Companies instance found for company ID: {companyId}, at UpdateCompany", updatedCompany.Id);
                        throw new CustomException($"Existing Companies instance retrieved for company ID: {updatedCompany.Id} is null at UpdateCompany.");
                    }

                }
                else
                {
                    _logger.LogWarning("Updated Companies instance passed is null. Failed to update Companies instance at UpdateCompany.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating Companies instance at UpdateCompany: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateCompany: " + exception + ".", exception);
            }
        }
    }
}
