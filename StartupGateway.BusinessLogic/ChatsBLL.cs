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
using StartupGateway.Shared;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model <see cref="Chats"/>.
    /// </summary>
    public class ChatsBLL
    {
        private readonly ILogger<ChatsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to initialize ChatsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public ChatsBLL(ILogger<ChatsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>Chats</c> instance information for the Id passed.
        /// </summary>
        /// <param name="chatId"></param>
        /// <returns>Instance of <see cref="Chats"/></returns>
        public Chats GetChatById(int chatId)
        {
            try
            {
                // Garbage Collection
                Chats? chats;
                using (IChatsDAL chatsDAL = _unitOfWork.GetDAL<IChatsDAL>()) 
                {
                    chats=chatsDAL.GetEntityById(chatId);
                }
                    
                if (chats != null)
                {
                    _logger.LogInformation("Chats instance retrieved succesfully for chat ID: {chatId}, at GetChatById.", chatId);
                    return chats;
                }
                else
                {
                    _logger.LogWarning("No Chats instance found for chat ID: {chatId}, at GetChatById", chatId);
                    throw new CustomException("Chats instance retrieved at GetChatById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Chats instance for chat ID: {bidDocumentId}, at GetChatById: {errorMessage}", chatId, exception);
                throw new CustomException("Exception Caught at GetChatById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all the chats.
        /// </summary>
        /// <returns>List of <see cref="Chats"/>? Instances</returns>
        public List<Chats>? GetAllChats()
        {
            try
            {
                // Garbage Collection
                List<Chats> listOfChats;
                using (IChatsDAL chatsDAL = _unitOfWork.GetDAL<IChatsDAL>()) 
                {
                    listOfChats=chatsDAL.GetAllRecords().ToList();
                }

                if (listOfChats != null)
                {
                    _logger.LogInformation("All Chats instances retrieved successfully at GetAllChats.");
                }
                else
                {
                    _logger.LogInformation("No Chats instances found at GetAllChats.");
                }
                return listOfChats;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving Chats instances at GetAllChats: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllChats: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>Chats</c> to the database.
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="Chats"/> added successfully.</returns>
        public bool AddChat(Chats chat, int userId)
        {
            try
            {
                if (chat != null)
                {
                    chat.Status = EntityStatus.Active;
                    chat.ModifiedBy = userId;
                    chat.ModifiedOn = DateTime.Now;

                    // GarbageCollection
                    using (IChatsDAL chatsDAL = _unitOfWork.GetDAL<IChatsDAL>())
                    {
                        chatsDAL.AddEntity(chat);
                    }                   
                    _unitOfWork.Commit();
                    _logger.LogInformation("New Chats instance successfully added at AddChat.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New Chats instance is null. Failed to add new Chats instance at AddChat.");
                    throw new CustomException("New BidDocuments instance is null. Failed to add new Chats instance at AddChat.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new Chats instance at AddChat: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddChat: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>Chats</c>.
        /// </summary>
        /// <param name="updatedChats"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdateChat(Chats updatedChats, int userId)
        {
            try
            {
                if (updatedChats != null)
                {
                    // Garbage Collection
                    Chats? existingChats;
                    using IChatsDAL chatsDAL = _unitOfWork.GetDAL<IChatsDAL>();
                    existingChats = chatsDAL.GetEntityById(updatedChats.Id);


                    if (existingChats != null)
                    {
                        existingChats.ChatTitle= !string.IsNullOrWhiteSpace(updatedChats.ChatTitle) ? updatedChats.ChatTitle : existingChats.ChatTitle;
                        existingChats.ChatBody = !string.IsNullOrWhiteSpace(updatedChats.ChatBody)? updatedChats.ChatBody : existingChats.ChatBody;

                        existingChats.Status = updatedChats.Status;
                        existingChats.ModifiedBy = userId;
                        existingChats.ModifiedOn = DateTime.Now;

                        chatsDAL.UpdateEntity(existingChats);
                        _unitOfWork.Commit();

                        _logger.LogInformation("Chats instance with ID: {chatId} updated successfully at UpdateChat.", existingChats.Id);
                        return true;
                    }
                    else
                    {
                        _logger.LogError("No Chats instance found for chat ID: {chatId}, at UpdateChat", updatedChats.Id);
                        throw new CustomException($"Existing Chats instance retrieved for bid document ID: {updatedChats.Id} is null at UpdateChat.");
                    }
                }
                else
                {
                    _logger.LogWarning("Updated Chats instance passed is null. Failed to update BidDocuments instance at UpdateChat.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating Chats instance at UpdateChat: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateChat: " + exception + ".", exception);
            }
        }
    }
}
