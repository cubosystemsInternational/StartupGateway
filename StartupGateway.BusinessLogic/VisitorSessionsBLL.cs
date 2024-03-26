/**
 * Modified by: Zaid
 * Created on: 03/21/2024
 * Description: Business logic class VisitorSessionBLL Modified.
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

namespace StartupGateway.BusinessLogic
{
    public class VisitorSessionsBLL
    {
        private readonly ILogger<VisitorSessionsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize VisitorSessionBLL with necessary dependencies.
        /// </summary>
        public VisitorSessionsBLL(ILogger<VisitorSessionsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the VisitorSession information for the Id passed.
        /// </summary>
        /// <param name="visitorSessionId"></param>
        /// <returns>VisitorSession?</returns>
        public VisitorSessions GetVisitorSessionById(int visitorSessionId)
        {
            try
            {
                var visitorSession = unitOfWork.GetDAL<IVisitorSessionsDAL>().GetEntityById(visitorSessionId);
                if (visitorSession != null)
                {
                    logger.LogInformation($"VisitorSession with ID {visitorSessionId} retrieved successfully.");
                    return visitorSession;
                }
                else
                {
                    // Log the absence of the VisitorSession and throw a KeyNotFoundException
                    var message = $"VisitorSession with ID {visitorSessionId} not found.";
                    logger.LogError(message);
                    throw new KeyNotFoundException(message);
                }
            }
            catch (Exception exception)
            {
                // Log and rethrow any other exceptions that occur during execution
                logger.LogError($"Exception caught at GetVisitorSessionById: {exception}.");
                throw;
            }
        }


        /// <summary>
        /// Retrieves all the visitor sessions.
        /// </summary>
        /// <returns>List of VisitorSession?</returns>
        public List<VisitorSessions>? GetAllVisitorSessions()
        {
            try
            {
                var listOfVisitorSessions = unitOfWork.GetDAL<IVisitorSessionsDAL>().GetAllRecords().ToList();
                logger.LogInformation("VisitorSessions retrieved successfully at GetAllVisitorSessions.");
                return listOfVisitorSessions;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllVisitorSessions: " + exception + ".");
                throw;
            }
        }

        /// <summary>
        /// Adds an instance of VisitorSession to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="visitorSession"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddVisitorSession(VisitorSessions visitorSession, int userId)
        {
            try
            {
                if (visitorSession != null)
                {
                    visitorSession.Status = EntityStatus.Active;
                    visitorSession.ModifiedBy = userId;
                    visitorSession.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IVisitorSessionsDAL>().AddEntity(visitorSession);
                    unitOfWork.Commit();
                    logger.LogInformation("VisitorSession successfully added at AddVisitorSession.");
                    return true;
                }
                else
                {
                    logger.LogInformation("VisitorSession is null at AddVisitorSession");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddVisitorSession: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of VisitorSession. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newVisitorSession"></param>
        /// <returns>True or False</returns>
        public bool UpdateVisitorSession(VisitorSessions newVisitorSession)
        {
            try
            {
                if (newVisitorSession != null)
                {
                    VisitorSessions existingVisitorSession = unitOfWork.GetDAL<IVisitorSessionsDAL>().GetEntityById(newVisitorSession.Id);

                    // Update attributes if new values are not null or whitespace
                    existingVisitorSession.Duration = newVisitorSession.Duration;
                    existingVisitorSession.IpAddress = newVisitorSession.IpAddress;
                    existingVisitorSession.Status = newVisitorSession.Status != EntityStatus.Pending ? newVisitorSession.Status : existingVisitorSession.Status;
                    existingVisitorSession.ModifiedOn = DateTime.Now;
                    existingVisitorSession.ModifiedBy = newVisitorSession.ModifiedBy != 0 ? newVisitorSession.ModifiedBy : existingVisitorSession.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("VisitorSession updated successfully at UpdateVisitorSession.");
                    return true;
                }
                else
                {
                    logger.LogInformation("VisitorSession passed at UpdateVisitorSession is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateVisitorSession: " + exception + ".");
                return false;
            }
        }
    }
}
