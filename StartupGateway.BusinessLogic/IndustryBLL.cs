/**
 * Created by: Zuhri
 * Created on: 19/03/2024
 * Description: Business logic class IndustryBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using Mysqlx;
using Mysqlx.Crud;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Implementation;
using StartupGateway.DAL.Interfaces;
using StartupGateway.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model IndustryBLL.
    /// </summary>
    public class IndustryBLL
    {
        private readonly ILogger<IndustryBLL> logger;
        private readonly UnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize IndustryBLL with necessary dependencies.
        /// </summary>
        public IndustryBLL(ILogger<IndustryBLL> logger, UnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Industry information for the Id passed.
        /// </summary>
        /// <param name="chatDetailId"></param>
        /// <returns>Industry?</returns>
        public Industry? GetIndustryById(int chatDetailId)
        {
            try
            {
                var Industry = unitOfWork.GetDAL<IndustryDAL>().GetEntityById(chatDetailId);
                if (Industry != null)
                {
                    logger.LogInformation("Industry retrieved succesfully at GetIndustryById.");
                    return Industry;
                }
                else
                {
                    logger.LogInformation("Industry retrieved at GetIndustryById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetIndustryById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the Industry.
        /// </summary>
        /// <returns>List of Industry?</returns>
        public List<Industry>? GetAllIndustry()
        {
            try
            {
                var listOfIndustry = unitOfWork.GetDAL<IndustryDAL>().GetAllRecords().ToList();
                logger.LogInformation("Industry retrieved successfully at GetAllIndustry.");
                return listOfIndustry;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllIndustry: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of Industry to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="Industry"></param>
        /// <returns>True or False</returns>
        public bool AddIndustry(Industry Industry)
        {
            try
            {
                if (Industry != null)
                {
                    unitOfWork.GetDAL<IndustryDAL>().AddEntity(Industry);
                    unitOfWork.Commit();
                    logger.LogInformation("Industry successfully added at AddIndustry.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Industry is null at AddIndustry");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddIndustry: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of Industry. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newIndustry"></param>
        /// <returns>True or False</returns>
        public bool UpdateIndustry(Industry newIndustry, int userId)
        {
            try
            {
                if (newIndustry != null)
                {
                    Industry existingIndustry = unitOfWork.GetDAL<IIndustryDAL>().GetEntityById(newIndustry.Id);
                    existingIndustry.Id = newIndustry.Id;
                    existingIndustry.IndustryName = newIndustry.IndustryName;
                    existingIndustry.Status = newIndustry.Status;
                    existingIndustry.ModifiedOn = DateTime.Now;
                    existingIndustry.ModifiedBy = userId;

                    unitOfWork.Commit();

                    logger.LogInformation("Industry updated successfully at UpdateIndustry.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Industry passed at UpdateIndustry are null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateIndustry: " + exception + ".");
                return false;
            }
        }
    }
}
