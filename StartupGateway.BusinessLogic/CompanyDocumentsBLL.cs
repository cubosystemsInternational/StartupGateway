/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class CompanyDocumentsBLL created.
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
    /// Business logic layer for managing operations related to model <see cref="CompanyDocuments"/>.
    /// </summary>
    public class CompanyDocumentsBLL
    {
        private readonly ILogger<ChatDetailsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize CompanyDocumentsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public CompanyDocumentsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>CompanyDocuments</c> instance information for the Id passed.
        /// </summary>
        /// <param name="companyDocumentsId"></param>
        /// <returns>Instance of <see cref="CompanyDocuments"/></returns>
        public CompanyDocuments GetCompanyDocumentById(int companyDocumentsId) 
        {
            try
            {
                // Garbage Collection
                CompanyDocuments? companyDocuments;
                using (ICompanyDocumentsDAL companyDocumentsDAL=_unitOfWork.GetDAL<ICompanyDocumentsDAL>())
                {
                   companyDocuments = companyDocumentsDAL.GetEntityById(companyDocumentsId);
                }

                if (companyDocuments != null)
                {
                    _logger.LogInformation("CompanyDocuments instance retrieved succesfully for company document ID: {companyDocumentId}, at GetCompanyDocumentById.", companyDocumentsId);
                    return companyDocuments;
                }
                else
                {
                    _logger.LogWarning("No CompanyDocuments instance found for company document ID: {companyDocumentId}, at GetCompanyDocumentById", companyDocumentsId);
                    throw new CustomException("CompanyDocuments instance retrieved at GetCompanyDocumentById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving CompanyDocuments instance for company document ID: {companyDocumentId}, at c: {errorMessage}", companyDocumentsId, exception);
                throw new CustomException("Exception Caught at GetCompanyDocumentById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>CompanyDocuments</c>.
        /// </summary>
        /// <returns>List of <see cref="CompanyDocuments"/>? Instances</returns>
        public List<CompanyDocuments>? GetAllCompanyDocuments() 
        {
            try
            {
                // Garbage Collection
                List<CompanyDocuments> listOfCompanyDocuments;
                using (ICompanyDocumentsDAL companyDocumentsDAL = _unitOfWork.GetDAL<ICompanyDocumentsDAL>()) 
                {
                    listOfCompanyDocuments = companyDocumentsDAL.GetAllRecords().ToList();
                }
                
                if (listOfCompanyDocuments != null)
                {
                    _logger.LogInformation("All CompanyDocuments instances retrieved successfully at GetAllCompanyDocuments.");
                }
                else 
                {
                    _logger.LogInformation("No CompanyDocuments instances found at GetAllCompanyDocuments.");
                }
                return listOfCompanyDocuments;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving CompanyDocuments instances at GetAllCompanyDocuments: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllCompanyDocuments: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>CompanyDocuments</c> to the Database. 
        /// </summary>
        /// <param name="companyDocuments"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="CompanyDocuments"/> added successfully.</returns>
        public bool AddCompanyDocument(CompanyDocuments companyDocuments, int userId) 
        {
            try
            {
                if (companyDocuments != null)
                {
                    companyDocuments.Status = EntityStatus.Active;
                    companyDocuments.ModifiedBy = userId;
                    companyDocuments.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (ICompanyDocumentsDAL companyDocumentsDAL = _unitOfWork.GetDAL<ICompanyDocumentsDAL>())
                    {
                        companyDocumentsDAL.AddEntity(companyDocuments);
                    }

                    _unitOfWork.Commit();
                    _logger.LogInformation("CompanyDetails instance successfully added at AddCompanyDocuments.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New CompanyDetails instance is null. Failed to add new CompanyDetails instance at AddCompanyDocument.");
                    throw new CustomException("New CompanyDetails instance is null. Failed to add new CompanyDetails instance at AddCompanyDocument.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new CompanyDetails instance at AddCompanyDocument: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddCompanyDocuments: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>CompanyDocuments</c>.
        /// </summary>
        /// <param name="newCompanyDocument"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdateCompanyDocument(CompanyDocuments updatedCompanyDocument,int userId) 
        {
            try
            {
                if (updatedCompanyDocument != null)
                {

                    // Garbage Colletion
                    using ICompanyDocumentsDAL companyDocumentsDAL = _unitOfWork.GetDAL<ICompanyDocumentsDAL>();
                    CompanyDocuments? existingCompanyDocument = companyDocumentsDAL.GetEntityById(updatedCompanyDocument.Id);

                    if (existingCompanyDocument != null)
                    {
                        existingCompanyDocument.DocumentType = !string.IsNullOrWhiteSpace(updatedCompanyDocument.DocumentType) ? updatedCompanyDocument.DocumentType : existingCompanyDocument.DocumentType;

                        existingCompanyDocument.Status = updatedCompanyDocument.Status;
                        existingCompanyDocument.ModifiedBy = userId;
                        existingCompanyDocument.ModifiedOn = DateTime.Now;

                        companyDocumentsDAL.UpdateEntity(existingCompanyDocument);

                        _unitOfWork.Commit();

                        _logger.LogInformation("CompanyDocuments instance with ID: {companyDocumentId} updated successfully at UpdateCompanyDocument.", existingCompanyDocument.Id);
                        return true;
                    }
                    else 
                    {
                        _logger.LogError("No CompanyDocuments instance found for company document ID: {companyDocumentId}, at UpdateCompanyDocument", updatedCompanyDocument.Id);
                        throw new CustomException($"Existing CompanyDocuments instance retrieved for company document ID: {updatedCompanyDocument.Id} is null at UpdateCompanyDocument.");
                    }

                }
                else
                {
                    _logger.LogWarning("Updated CompanyDocuments instance passed is null. Failed to update CompanyDocuments instance at UpdateCompanyDocument.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating CompanyDocuments instance at UpdateCompanyDocument: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateCompanyDocument: " + exception + ".", exception);
            }
        }
    
    }
}
