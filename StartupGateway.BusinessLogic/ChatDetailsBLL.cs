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
    /// Business logic layer for managing operations related to model <see cref="ChatDetails"/>.
    /// </summary>
    public class ChatDetailsBLL
    {
        private readonly ILogger<ChatDetailsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize <c>ChatDetailsBLL</c> with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public ChatDetailsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the ChatDetails instance information for the id passed.
        /// </summary>
        /// <param name="chatDetailsId"></param>
        /// <returns>Instance of <see cref="ChatDetails"/></returns>
        public ChatDetails GetChatDetailById(int chatDetailsId)
        {
            try
            {
                // Garbage Collection
                ChatDetails? chatDetails;
                using (IChatDetailsDAL chatDetailsDAL = _unitOfWork.GetDAL<IChatDetailsDAL>()) 
                {
                    chatDetails= chatDetailsDAL.GetEntityById(chatDetailsId);
                }

                if (chatDetails!=null) 
                {
                    _logger.LogInformation("ChatDetails instance retrieved succesfully for chat detail ID: {chatDetailsId}, at GetChatDetailById.", chatDetailsId);
                    return chatDetails;
                }
                else
                {
                    _logger.LogWarning("No ChatDetails instance found for chat details ID: {chatDetailsId}, at GetChatDetailById", chatDetailsId);
                    throw new CustomException("ChatDetails instance retrieved at GetChatDetailById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving ChatDetails instance for chat details ID: {chatDetailsId}, at GetChatDetailById: {errorMessage}", chatDetailsId, exception);
                throw new CustomException("Exception Caught at GetChatDetailById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of ChatDetails.
        /// </summary>
        /// <returns>List of <see cref="ChatDetails"/>? Instances</returns>
        public List<ChatDetails>? GetAllChatDetails()
        {
            try 
            {
                // Garbage Collection
                List<ChatDetails> listOfChatDetails;
                using (IChatDetailsDAL chatDetailsDAL = _unitOfWork.GetDAL<IChatDetailsDAL>()) 
                {
                    listOfChatDetails=chatDetailsDAL.GetAllRecords().ToList();
                }

                if (listOfChatDetails != null)
                {
                    _logger.LogInformation("All ChatDetails instances retrieved successfully at GetAllChatDetails.");
                }
                else 
                {
                    _logger.LogInformation("No ChatDetails instances found at GetAllChatDetails.");
                }
                return listOfChatDetails;
            }
            catch(Exception exception) 
            {
                _logger.LogError(exception, "Error retrieving ChatDetails instances at GetAllChatDetails: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllChatDetails: " + exception + ".",exception);
            }
        }

        /// <summary>
        /// Adds an instance of ChatDetails to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="chatdetails"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="ChatDetails"/> added successfully.</returns>
        public bool AddChatDetail( ChatDetails chatdetails, int userId)
        {
            try 
            {
                if (chatdetails != null)
                {
                    chatdetails.Status = EntityStatus.Active;
                    chatdetails.ModifiedBy = userId;
                    chatdetails.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (IChatDetailsDAL chatDetailsDAL = _unitOfWork.GetDAL<IChatDetailsDAL>()) 
                    {
                        chatDetailsDAL.AddEntity(chatdetails);
                    }
                    _unitOfWork.Commit();
                    _logger.LogInformation("New ChatDetails instance successfully added at AddChatDetail.");
                    return true;
                }
                else 
                {
                    _logger.LogWarning("New ChatDetails instance is null. Failed to add new ChatDetails instance at AddChatDetail.");
                    throw new CustomException("New ChatDetails instance is null. Failed to add new ChatDetails instance at AddChatDetail.");
                }
                
            }
            catch (Exception exception) 
            {
                _logger.LogError(exception, "Error adding new ChatDetails instance at AddChatDetail: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddChatDetail: " + exception + ".",exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of ChatDetails. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newChatDetails"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdateChatDetail(ChatDetails updatedChatDetails, int userId) 
        {
            try 
            {              
                if (updatedChatDetails != null)
                {

                    // Garbage Collection
                    ChatDetails? existingChatDetails;
                    using IChatDetailsDAL chatDetailsDAL = _unitOfWork.GetDAL<IChatDetailsDAL>();
                    existingChatDetails = chatDetailsDAL.GetEntityById(updatedChatDetails.Id);


                    if (existingChatDetails != null)
                    {

                        existingChatDetails.Attachment = updatedChatDetails.Attachment;

                        existingChatDetails.Status = existingChatDetails.Status;
                        existingChatDetails.ModifiedBy = existingChatDetails.UserId;
                        existingChatDetails.ModifiedOn = DateTime.Now;

                        chatDetailsDAL.UpdateEntity(existingChatDetails);

                        _unitOfWork.Commit();

                        _logger.LogInformation("ChatDetails instance with ID: {chatDetailsId} updated successfully at UpdateChatDetail.", existingChatDetails.Id);
                        return true;
                    }
                    else
                    {
                        _logger.LogError("No ChatDetails instance found for chat detail ID: {bidDocumentId}, at UpdateChatDetail", updatedChatDetails.Id);
                        throw new CustomException($"Existing ChatDetails instance retrieved for chat detail ID: {updatedChatDetails.Id} is null at updatedBidDocument.");

                    }

                }
                else
                {
                    _logger.LogWarning("Updated ChatDetails instance passed is null. Failed to update ChatDetails instance at UpdateChatDetail.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception) 
            {
                _logger.LogInformation(exception, "Error updating ChatDetails instance at UpdateChatDetail: {errorMessage}", exception);
                throw new CustomException("Exception Caught at UpdateChatDetail: " + exception + ".");   
            }
        }
    }
}
