/**
 * Created by: Shuaib
 * Created on: 21/03/2024
 * Description: Business logic class CommunicationsBLL created.
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
    /// Business logic layer for managing operations related to model CommunicationsBLL.
    /// </summary>
    public class CommunicationsBLL
    {
        private readonly ILogger<CommunicationsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize CommunicationsBLL with necessary dependencies.
        /// <paramref name="logger"/>
        /// <paramref name="unitOfWork"/>
        /// </summary>
        public CommunicationsBLL(ILogger<CommunicationsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Communications instance information for the id passed.
        /// </summary>
        /// <param name="communicationsId"></param>
        /// <returns>Communications</returns>
        public Communications GetCommunicationsById(int communicationsId) 
        {
            try
            {
                var communications = unitOfWork.GetDAL<ICommunicationsDAL>().GetEntityById(communicationsId);
                if (communications != null)
                {
                    logger.LogInformation("Communications instance retrieved succesfully at GetCommunicationsById.");
                    return communications;
                }
                else
                {
                    logger.LogInformation("Communications instance retrieved at GetCommunicationsById is null.");
                    throw new CustomException("Communications instance retrieved at GetCommunicationsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetCommunicationsById: " + exception + ".");
                throw new CustomException("Exception Caught at GetCommunicationsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieves all instances of Communications.
        /// </summary>
        /// <returns>List of Communications</returns>
        public List<Communications> GetAllCommunications() 
        {
            try
            {
                var listOfCommunications = unitOfWork.GetDAL<ICommunicationsDAL>().GetAllRecords().ToList();
                if (listOfCommunications != null)
                {
                    logger.LogInformation("Communications instances retrieved successfully at GetAllCommunications.");
                    return listOfCommunications;
                }
                else
                {
                    logger.LogInformation("Instances of Communications retrieved is null at GetAllCommunications.");
                    throw new CustomException("Instances of Communications retrieved is null at GetAllCommunications.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllCommunications: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllCommunications: " + exception + ".");
            }
        }



        /// <summary>
        /// Adds an instance of AddCommmunications to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="communications"></param>
        /// <returns>True or False</returns>
        public bool AddCommmunications(Communications communications) 
        {
            try
            {
                if (communications != null)
                {
                    communications.Status = EntityStatus.Active;
                    communications.ModifiedBy = communications.UserId;
                    communications.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ICommunicationsDAL>().AddEntity(communications);
                    unitOfWork.Commit();
                    logger.LogInformation("Communications instance successfully added at AddCommmunications.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Communications instance is null at AddCommmunications.");
                    throw new CustomException("Communications instance is null at AddCommmunications.");
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddCommmunications: " + exception + ".");
                throw new CustomException("Exception Caught at AddCommmunications: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of UpdateCommunications. Returns True, if the update operation was successfull
        /// </summary>
        /// <param name="newCommunications"></param>
        /// <returns>True of False</returns>
        public bool UpdateCommunications(Communications newCommunications) 
        {
            try
            {
                if (newCommunications != null)
                {
                    Communications existingCommunications = unitOfWork.GetDAL<ICommunicationsDAL>().GetEntityById(newCommunications.Id);

                    existingCommunications.Subject = newCommunications.Subject;
                    existingCommunications.Message = newCommunications.Message?? null;

                    existingCommunications.Status = newCommunications.Status;
                    existingCommunications.ModifiedBy = newCommunications.UserId;
                    existingCommunications.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ICommunicationsDAL>().UpdateEntity(existingCommunications);
                    unitOfWork.Commit();

                    logger.LogInformation("Communications instance updated successfully at UpdateCommunications.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Communications instance passed at UpdateCommunications are null.");
                    throw new CustomException("Communications instance passed at UpdateCommunications are null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateCommunications: " + exception + ".");
                throw new CustomException("Exception Caught at UpdateCommunications: " + exception + ".");
            }
        }
    }
}
