/**
 * Created by: Shuaib
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
using StartupGateway.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model BidDocumentsBLL.
    /// </summary>
    public class BidDocumentsBLL
    {
        private readonly ILogger<BidDocumentsBLL> logger;
        private readonly UnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize BidDocumentsBLL with necessary dependencies.
        /// </summary>
        public BidDocumentsBLL(ILogger<BidDocumentsBLL> logger, UnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Bid Documents information for the Id passed.
        /// </summary>
        /// <param name="chatDetailId"></param>
        /// <returns>BidDocuments?</returns>
        public BidDocuments? GetBidDocumentsById(int chatDetailId)
        {
            try
            {
                var BidDocuments = unitOfWork.GetDAL<BidDocumentsDAL>().GetEntityById(chatDetailId);
                if (BidDocuments != null)
                {
                    logger.LogInformation("Bid Documents retrieved succesfully at GetBidDocumentsById.");
                    return BidDocuments;
                }
                else
                {
                    logger.LogInformation("Bid Documents retrieved at GetBidDocumentsById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetBidDocumentsById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the Bid Documents.
        /// </summary>
        /// <returns>List of BidDocuments?</returns>
        public List<BidDocuments>? GetAllBidDocuments()
        {
            try
            {
                var listOfBidDocuments = unitOfWork.GetDAL<BidDocumentsDAL>().GetAllRecords().ToList();
                logger.LogInformation("Bid Documents retrieved successfully at GetAllBidDocuments.");
                return listOfBidDocuments;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllBidDocuments: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of BidDocuments to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="BidDocuments"></param>
        /// <returns>True or False</returns>
        public bool AddBidDocuments(BidDocuments BidDocuments)
        {
            try
            {
                if (BidDocuments != null)
                {
                    unitOfWork.GetDAL<BidDocumentsDAL>().AddEntity(BidDocuments);
                    unitOfWork.Commit();
                    logger.LogInformation("Bid Documents successfully added at AddBidDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Bid Documents is null at AddBidDocuments");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddBidDocuments: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of BidDocuments. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newBidDocuments"></param>
        /// <returns>True or False</returns>
        public bool UpdateBidDocuments(BidDocuments newBidDocuments, int userId)
        {
            try
            {
                if (newBidDocuments != null)
                {
                    BidDocuments existingBidDocuments = unitOfWork.GetDAL<BidDocumentsDAL>().GetEntityById(newBidDocuments.BidDocumentId);

                    existingBidDocuments.Status = newBidDocuments.Status;
                    existingBidDocuments.ModifiedOn = DateTime.Now;
                    existingBidDocuments.ModifiedBy = userId;

                    unitOfWork.Commit();

                    logger.LogInformation("Bid Documents updated successfully at UpdateBidDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Bid Documents passed at UpdateBidDocuments are null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateBidDocuments: " + exception + ".");
                return false;
            }
        }
    }
}
