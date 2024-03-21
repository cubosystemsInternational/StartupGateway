/**
 * Created by: Shuaib
 * Created on: 19/03/2024
 * Description: Business logic class ChatDetailsBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using Mysqlx;
using Mysqlx.Crud;
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
    /// Business logic layer for managing operations related to model ChatDetailsBLL.
    /// </summary>
    public class ChatDetailsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize ChatDetailsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public ChatDetailsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the ChatDetails instance information for the id passed.
        /// </summary>
        /// <param name="chatDetailId"></param>
        /// <returns>ChatDetails?</returns>
        public ChatDetails GetChatDetailsById(int chatDetailId)
        {
            try
            {
                var chatDetails = unitOfWork.GetDAL<IChatDetailsDAL>().GetEntityById(chatDetailId);
                if (chatDetails!=null) 
                {
                    logger.LogInformation("ChatDetails instance retrieved succesfully at GetChatDetailsById.");
                    return chatDetails;
                }
                else
                {
                    logger.LogInformation("ChatDetails instance retrieved at GetChatDetailsById is null.");
                    throw new CustomException("ChatDetails instance retrieved at GetChatDetailsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetChatDeatilsById: "+exception+".");
                throw new CustomException("Exception Caught at GetChatDeatilsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieves all instances of ChatDetails.
        /// </summary>
        /// <returns>List of ChatDetails?</returns>
        public List<ChatDetails> GetAllChatDetails()
        {
            try 
            {
                var listOfChatDetails=unitOfWork.GetDAL<ChatDetailsDAL>().GetAllRecords().ToList();
                if (listOfChatDetails != null)
                {
                    logger.LogInformation("ChatDetails instances retrieved successfully at GetAllChatDetails.");
                    return listOfChatDetails;
                }
                else 
                {
                    logger.LogInformation("Instances of ChatDetails retrieved is null at GetAllChatDetails.");
                    throw new CustomException("Instances of ChatDetails retrieved is null at GetAllChatDetails.");
                }
            }
            catch(Exception exception) 
            {
                logger.LogInformation("Exception Caught at GetAllChatDetails: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllChatDetails: " + exception + ".");
            }
        }

        /// <summary>
        /// Adds an instance of ChatDetails to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="chatdetails"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddChatDetails( ChatDetails chatdetails, int userId)
        {
            try 
            {
                if (chatdetails != null)
                {
                    chatdetails.Status = EntityStatus.Active;
                    chatdetails.ModifiedBy = userId;
                    chatdetails.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<ChatDetailsDAL>().AddEntity(chatdetails);
                    unitOfWork.Commit();
                    logger.LogInformation("ChatDetails instance successfully added at AddChatDetails.");
                    return true;
                }
                else 
                {
                    logger.LogInformation("ChatDetails instance is null at AddChatDetails.");
                    throw new CustomException("Chat details is null at AddChatDetails.");
                }
                
            }
            catch (Exception exception) 
            {
                logger.LogInformation("Exception Caught at AddChatDetails: " + exception + ".");
                throw new CustomException("Exception Caught at AddChatDetails: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of ChatDetails. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newChatDetails"></param>
        /// <returns>True or False</returns>
        public bool UpdateChatDetails(ChatDetails newChatDetails) 
        {
            try 
            {              
                if (newChatDetails != null)
                {
                    ChatDetails existingChatDetails = unitOfWork.GetDAL<ChatDetailsDAL>().GetEntityById(newChatDetails.ChatDetailsId);

                    existingChatDetails.Status = newChatDetails.Status;
                    existingChatDetails.Attachment = newChatDetails.Attachment;
                    existingChatDetails.ModifiedOn = DateTime.Now;
                    existingChatDetails.ModifiedBy = newChatDetails.UserId;

                    unitOfWork.GetDAL<ChatDetailsDAL>().UpdateEntity(existingChatDetails);
                    unitOfWork.Commit();

                    logger.LogInformation("ChatDetails instance updated successfully at UpdateChatDetails.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ChatDetails instance passed at UpdateChatDetails are null.");
                    throw new CustomException("ChatDetails instance passed at UpdateChatDetails are null.");
                }
            }
            catch (Exception exception) 
            {
                logger.LogInformation("Exception Caught at UpdateChatDetails: " + exception + ".");
                throw new CustomException("Exception Caught at UpdateChatDetails: " + exception + ".");   
            }
        }
    }
}
