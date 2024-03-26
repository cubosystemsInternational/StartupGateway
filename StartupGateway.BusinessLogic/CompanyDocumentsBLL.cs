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
    /// Business logic layer for managing company documents.
    /// </summary>
    public class CompanyDocumentsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize CompanyDocumentsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public CompanyDocumentsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the CompanyDocument instance information for the id passed.
        /// </summary>
        /// <param name="companyDocumentId"></param>
        /// <returns>CompanyDocuments?</returns>
        public CompanyDocuments GetCompanyDocumentsById(int companyDocumentId) 
        {
            try
            {
                var companyDocuments = unitOfWork.GetDAL<ICompanyDocumentsDAL>().GetEntityById(companyDocumentId);
                if (companyDocuments != null)
                {
                    logger.LogInformation("CompanyDetails instance retrieved succesfully at GetCompanyDocumentsById.");
                    return companyDocuments;
                }
                else
                {
                    logger.LogInformation("CompanyDetails instance retrieved at GetCompanyDocumentsById is null.");
                    throw new CustomException("CompanyDetails instance retrieved at GetCompanyDocumentsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetCompanyDocumentsById: " + exception + ".");
                throw new CustomException("Exception Caught at GetCompanyDocumentsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieves all instances of CompanyDocuments.
        /// </summary>
        /// <returns>List of CompanyDetails</returns>
        public List<CompanyDocuments> GetAllCompanyDocuments() 
        {
            try
            {
                var listOfCompanyDocuments = unitOfWork.GetDAL<ICompanyDocumentsDAL>().GetAllRecords().ToList();
                
                if (listOfCompanyDocuments != null)
                {
                    logger.LogInformation("CompanyDetails instances retrieved successfully at GetAllCompanyDocuments.");
                    return listOfCompanyDocuments;
                }
                else 
                {
                    logger.LogInformation("Instance of CompanyDetails retrieved is null at GetAllCompanyDocuments.");
                    throw new CustomException("Instance of CompanyDetails retrieved is null at GetAllCompanyDocuments.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllCompanyDocuments: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllCompanyDocuments: " + exception + ".");
            }
        }

        /// <summary>
        /// Adds an instance of CompanyDocuments to the Database. Returns True if operation was successful.
        /// </summary>
        /// <param name="companyDocuments"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddCompanyDocuments(CompanyDocuments companyDocuments, int userId) 
        {
            try
            {
                if (companyDocuments != null)
                {
                    companyDocuments.Status = EntityStatus.Active;
                    companyDocuments.ModifiedBy = userId;
                    companyDocuments.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ICompanyDocumentsDAL>().AddEntity(companyDocuments);
                    unitOfWork.Commit();
                    logger.LogInformation("CompanyDetails instance successfully added at AddCompanyDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("CompanyDetails instance is null at AddCompanyDocuments.");
                    throw new CustomException("CompanyDetails instance is null at AddCompanyDocuments.");
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddCompanyDocuments: " + exception + ".");
                throw new CustomException("Exception Caught at AddCompanyDocuments: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of CompanyDocuments. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="newCompanyDetails"></param>
        /// <returns>True or False</returns>
        public bool UpdateCompanyDocuments(CompanyDocuments newCompanyDocument,int userId) 
        {
            try
            {
                if (newCompanyDocument != null)
                {
                    CompanyDocuments existingCompanyDocuments = unitOfWork.GetDAL<ICompanyDocumentsDAL>().GetEntityById(newCompanyDocument.Id);

                    existingCompanyDocuments.DocumentType= newCompanyDocument.DocumentType;
                    existingCompanyDocuments.Status = newCompanyDocument.Status;
                    existingCompanyDocuments.ModifiedOn = DateTime.Now;
                    existingCompanyDocuments.ModifiedBy = userId;

                    unitOfWork.GetDAL<ICompanyDocumentsDAL>().UpdateEntity(existingCompanyDocuments);
                    unitOfWork.Commit();

                    logger.LogInformation("CompanyDetails instance updated successfully at UpdateCompanyDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("CompanyDetails instance passed at UpdateCompanyDocuments are null.");
                    throw new CustomException("CompanyDetails instance passed at UpdateCompanyDocuments are null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateCompanyDocuments: " + exception + ".");
                throw new CustomException("Exception Caught at UpdateCompanyDocuments: " + exception + ".");
            }
        }
    
    }
}
