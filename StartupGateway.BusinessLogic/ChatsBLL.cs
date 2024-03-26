﻿using System;
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
        public Chats GetChatById(int chatId)
        {
            try
            {
                var chat = unitOfWork.GetDAL<IChatsDAL>().GetEntityById(chatId);
                if (chat != null)
                {
                    logger.LogInformation("Chat retrieved successfully at GetChatById.");
                    return chat;
                }
                else
                {
                    logger.LogInformation("Chat retrieved at GetChatById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetChatById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the chats.
        /// </summary>
        /// <returns>List of Chat?</returns>
        public List<Chats> GetAllChats()
        {
            try
            {
                var listOfChats = unitOfWork.GetDAL<IChatsDAL>().GetAllRecords().ToList();
                logger.LogInformation("Chats retrieved successfully at GetAllChats.");
                return listOfChats;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception caught at GetAllChats: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of Chat to the database. Returns True if the operation was successful.
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddChat(Chats chat, int userId)
        {
            try
            {
                if (chat != null)
                {
                    chat.Status = EntityStatus.Active;
                    chat.ModifiedBy = userId;
                    chat.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IChatsDAL>().AddEntity(chat);
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
        public bool UpdateChat(Chats newChat)
        {
            try
            {
                if (newChat != null)
                {
                    Chats existingChat = unitOfWork.GetDAL<IChatsDAL>().GetEntityById(newChat.Id);

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
