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
using StartupGateway.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model ChatDetailsBLL.
    /// </summary>
    public class ChatDetailsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly UnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize ChatDetailsBLL with necessary dependencies.
        /// </summary>
        public ChatDetailsBLL(ILogger<ChatDetailsBLL> logger, UnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Chat Details information for the Id passed.
        /// </summary>
        /// <param name="chatDetailId"></param>
        /// <returns>ChatDetails?</returns>
        public ChatDetails? GetChatDetailsById(int chatDetailId)
        {
            try
            {
                var chatDetails = unitOfWork.GetRepository<ChatDetailsDAL>().GetEntityById(chatDetailId);
                if (chatDetails!=null) 
                {
                    logger.LogInformation("Chat details retrieved succesfully at GetChatDetailsById.");
                    return chatDetails;
                }
                else
                {
                    logger.LogInformation("Chat details retrieved at GetChatDetailsById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetChatDeatilsById: "+exception+".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the chat details.
        /// </summary>
        /// <returns>List of ChatDetails?</returns>
        public List<ChatDetails>? GetAllChatDetails()
        {
            try 
            {
                var listOfChatDetails=unitOfWork.GetRepository<ChatDetailsDAL>().GetAllRecords().ToList();
                logger.LogInformation("Chat details retrieved successfully at GetAllChatDetails.");
                return listOfChatDetails;
            }
            catch(Exception exception) 
            {
                logger.LogInformation("Exception Caught at GetAllChatDetails: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of ChatDetails to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="chatdetails"></param>
        /// <returns>True or False</returns>
        public bool AddChatDetails( ChatDetails chatdetails)
        {
            try 
            {
                if (chatdetails != null)
                {
                    unitOfWork.GetRepository<ChatDetailsDAL>().AddEntity(chatdetails);
                    unitOfWork.Commit();
                    logger.LogInformation("Chat details successfully added at AddChatDetails.");
                    return true;
                }
                else 
                {
                    logger.LogInformation("Chat details is null at AddChatDetails");
                    return false;
                }
                
            }
            catch (Exception exception) 
            {
                logger.LogInformation("Exception Caught at AddChatDetails: " + exception + ".");
                return false;
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
                    ChatDetails existingChatDetails = unitOfWork.GetRepository<ChatDetailsDAL>().GetEntityById(newChatDetails.ChatId);

                    existingChatDetails.Status = newChatDetails.Status;
                    existingChatDetails.Attachment = newChatDetails.Attachment;
                    existingChatDetails.ModifiedOn = DateTime.Now;
                    existingChatDetails.ModifiedBy = newChatDetails.UserId;

                    unitOfWork.Commit();

                    logger.LogInformation("Chat Details updated successfully at UpdateChatDetails.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Chat details passed at UpdateChatDetails are null.");
                    return false;
                }
            }
            catch (Exception exception) 
            {
                logger.LogInformation("Exception Caught at UpdateChatDetails: " + exception + ".");
                return false;
            }
        }
    }
}
