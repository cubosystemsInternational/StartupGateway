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

namespace StartupGateway.BusinessLogic
{
    public class CompaniesBLL
    {
        private readonly ILogger<CompaniesBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize CompaniesBLL with necessary dependencies.
        /// </summary>
        public CompaniesBLL(ILogger<CompaniesBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Company information for the Id passed.
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns>Company?</returns>
        public Company GetCompanyById(int companyId)
        {
            try
            {
                var company = unitOfWork.GetRepository<ICompanyDAL>().GetEntityById(companyId);
                if (company != null)
                {
                    logger.LogInformation("Company retrieved successfully at GetCompanyById.");
                    return company;
                }
                else
                {
                    logger.LogInformation("Company retrieved at GetCompanyById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetCompanyById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the companies.
        /// </summary>
        /// <returns>List of Company?</returns>
        public List<Company> GetAllCompanies()
        {
            try
            {
                var listOfCompanies = unitOfWork.GetRepository<ICompanyDAL>().GetAllRecords().ToList();
                logger.LogInformation("Companies retrieved successfully at GetAllCompanies.");
                return listOfCompanies;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllCompanies: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of Company to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="company"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddCompany(Company company, int userId)
        {
            try
            {
                if (company != null)
                {
                    company.Status = EntityStatus.Active;
                    company.ModifiedBy = userId;
                    company.ModifiedOn = DateTime.Now;

                    unitOfWork.GetRepository<ICompanyDAL>().AddEntity(company);
                    unitOfWork.Commit();
                    logger.LogInformation("Company successfully added at AddCompany.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Company is null at AddCompany");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddCompany: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of Company. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newCompany"></param>
        /// <returns>True or False</returns>
        public bool UpdateCompany(Company newCompany)
        {
            try
            {
                if (newCompany != null)
                {
                    Company existingCompany = unitOfWork.GetRepository<ICompanyDAL>().GetEntityById(newCompany.CompanyId);

                    // Update attributes if new values are not null or whitespace
                    existingCompany.CompanyName = !string.IsNullOrWhiteSpace(newCompany.CompanyName) ? newCompany.CompanyName : existingCompany.CompanyName;
                    existingCompany.Description = newCompany.Description ?? existingCompany.Description; // Update only if not null
                    existingCompany.Status = newCompany.Status != EntityStatus.Pending ? newCompany.Status : existingCompany.Status;
                    existingCompany.ModifiedOn = DateTime.Now;
                    existingCompany.ModifiedBy = newCompany.ModifiedBy != 0 ? newCompany.ModifiedBy : existingCompany.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("Company updated successfully at UpdateCompany.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Company passed at UpdateCompany is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateCompany: " + exception + ".");
                return false;
            }
        }
    }
}
