/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class ComsDocumentsBLL created.
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
    /// Business logic layer for managing operations related to model ComsDocument.
    /// </summary>
    public class CommunicationDocumentsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize ComsDocumentsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public CommunicationDocumentsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieve the ComsDocument instance information for the id passed.
        /// </summary>
        /// <param name="comsDocumentId"></param>
        /// <returns>ComsDocuments?</returns>
        public CommunicationDocuments GetComsDocumentsById(int comsDocumentId)
        {
            try
            {
                var comsDocuments = unitOfWork.GetDAL<ICommunicationDocumentsDAL>().GetEntityById(comsDocumentId);
                if (comsDocuments != null)
                {
                    logger.LogInformation("ComsDocuments instance retrieved succesfully at GetComsDocumentsById.");
                    return comsDocuments;
                }
                else
                {
                    logger.LogInformation("ComsDocuments instance retrieved at GetComsDocumentsById is null.");
                    throw new CustomException("ComsDocuments instance retrieved at GetComsDocumentsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetComsDocumentsById: " + exception + ".");
                throw new CustomException("Exception Caught at GetComsDocumentsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieve all instances of ComsDocuments.
        /// </summary>
        /// <returns>List of ComsDocuments?</returns>
        public List<CommunicationDocuments> GetAllComsDocuments()
        {
            try
            {
                var listOfComsDocuments = unitOfWork.GetDAL<ICommunicationDocumentsDAL>().GetAllRecords().ToList();
                if (listOfComsDocuments != null)
                {
                    logger.LogInformation("ComsDocuments instances retrieved successfully at GetAllComsDocuments.");
                    return listOfComsDocuments;
                }
                else 
                {
                    logger.LogInformation("Instances of ComsDocuments retrieved is null at GetAllComsDocuments.");
                    throw new CustomException("Instances of ComsDocuments retrieved is null at GetAllComsDocuments.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllComsDocuments: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllComsDocuments: " + exception + ".");
            }
        }

        /// <summary>
        /// Adds instance of ComsDocuments to the Database. Returns True if operation was successful.
        /// </summary>
        /// <param name="comsDocument">New Instace of ComsDocument to be saved.</param>
        /// <returns>True or False</returns>
        public bool AddComsDocuments(CommunicationDocuments comsDocument)
        {
            try
            {
                if (comsDocument != null)
                {
                    comsDocument.Status = EntityStatus.Active;
                    comsDocument.ModifiedBy = comsDocument.UserId;
                    comsDocument.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ICommunicationDocumentsDAL>().AddEntity(comsDocument);
                    unitOfWork.Commit();
                    logger.LogInformation("ComsDocuments instance successfully added at AddComsDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ComsDocuments instance is null at AddComsDocuments.");
                    throw new CustomException("ComsDocuments instance is null at AddComsDocuments.");
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddComsDocuments: " + exception + ".");
                throw new CustomException("Exception Caught at AddComsDocuments: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of ComsDocuments. Returns True if the update operation was successful.
        /// </summary>
        /// <param name="comsDocument"></param>
        /// <returns>True or False</returns>
        public bool UpdateComsDocuments(CommunicationDocuments newComsDocument)
        {
            try
            {
                if (newComsDocument != null)
                {
                    CommunicationDocuments existingComsDocument = unitOfWork.GetDAL<ICommunicationDocumentsDAL>().GetEntityById(newComsDocument.Id);

                    existingComsDocument.Status = newComsDocument.Status;
                    existingComsDocument.ModifiedBy = newComsDocument.UserId;
                    existingComsDocument.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ICommunicationDocumentsDAL>().UpdateEntity(existingComsDocument);
                    unitOfWork.Commit();

                    logger.LogInformation("ComsDocuments instance updated successfully at UpdateComsDocuments.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ComsDocuments instance passed at UpdateComsDocuments are null.");
                    throw new CustomException("ComsDocuments instance passed at UpdateComsDocuments are null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateComsDocuments: " + exception + ".");
                throw new CustomException("Exception Caught at UpdateComsDocuments: " + exception + ".");
            }
        }


    }
}
