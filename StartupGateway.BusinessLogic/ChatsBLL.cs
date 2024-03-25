/**
 * Modified by: Zaid
 * Created on: 03/21/2024
 * Description: Business logic class ChatsBLL Modified.
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
    public class ChatsBLL
    {
        private readonly ILogger<ChatsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to initialize ChatsBLL with necessary dependencies.
        /// </summary>
        public ChatsBLL(ILogger<ChatsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the Chat information for the Id passed.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns>Chat?</returns>
        public Chat GetChatById(int chatId)
        {
            try
            {
                var chat = unitOfWork.GetDAL<IChatDAL>().GetEntityById(chatId);
                if (chat != null)
                {
                    logger.LogInformation($"Chat with ID {chatId} retrieved successfully.");
                    return chat;
                }
                else
                {
                    // Log the absence of the Chat entity and throw a KeyNotFoundException
                    var message = $"Chat with ID {chatId} not found.";
                    logger.LogError(message);
                    throw new KeyNotFoundException(message);
                }
            }
            catch (Exception exception)
            {
                // Log and rethrow any other exceptions that occur during execution
                logger.LogError($"Exception caught at GetChatById: {exception}.");
                throw;
            }
        }


        /// <summary>
        /// Retrieves all the chats.
        /// </summary>
        /// <returns>List of Chat?</returns>
        public List<Chat> GetAllChats()
        {
            try
            {
                var listOfChats = unitOfWork.GetDAL<IChatDAL>().GetAllRecords().ToList();
                logger.LogInformation("Chats retrieved successfully at GetAllChats.");
                return listOfChats;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllChats: " + exception + ".");
                throw;
            }
        }

        /// <summary>
        /// Adds an instance of Chat to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddChat(Chat chat, int userId)
        {
            try
            {
                if (chat != null)
                {
                    chat.Status = EntityStatus.Active;
                    chat.ModifiedBy = userId;
                    chat.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IChatDAL>().AddEntity(chat);
                    unitOfWork.Commit();
                    logger.LogInformation("Chat successfully added at AddChat.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Chat is null at AddChat");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at AddChat: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of Chat. Returns True, if the update operation was successful.
        /// </summary>
        /// <param name="newChat"></param>
        /// <returns>True or False</returns>
        public bool UpdateChat(Chat newChat)
        {
            try
            {
                if (newChat != null)
                {
                    Chat existingChat = unitOfWork.GetDAL<IChatDAL>().GetEntityById(newChat.ChatId);

                    // Update attributes if new values are not null or whitespace
                    existingChat.ChatTitle = !string.IsNullOrWhiteSpace(newChat.ChatTitle) ? newChat.ChatTitle : existingChat.ChatTitle;
                    existingChat.ChatBody = !string.IsNullOrWhiteSpace(newChat.ChatBody) ? newChat.ChatBody : existingChat.ChatBody;
                    existingChat.Status = newChat.Status != EntityStatus.Pending ? newChat.Status : existingChat.Status;
                    existingChat.ModifiedOn = DateTime.Now;
                    existingChat.ModifiedBy = newChat.ModifiedBy != 0 ? newChat.ModifiedBy : existingChat.ModifiedBy;

                    unitOfWork.Commit();

                    logger.LogInformation("Chat updated successfully at UpdateChat.");
                    return true;
                }
                else
                {
                    logger.LogInformation("Chat passed at UpdateChat is null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at UpdateChat: " + exception + ".");
                return false;
            }
        }
    }
}
