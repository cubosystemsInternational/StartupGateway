/**
 * Created by: Shuaib
 * Created on: 21/03/2024
 * Description: Business logic class ContactUsBLL created.
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
using StartupGateway.Shared;
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
    /// Business logic layer for managing operations related to model <see cref="ContactUs"/>.
    /// </summary>
    public class ContactUsBLL
    {
        private readonly ILogger<ChatDetailsBLL> _logger;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize ContactUsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public ContactUsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>ContactUs</c> instance information for the Id passed.
        /// </summary>
        /// <param name="contactUsId"></param>
        /// <returns>Instance of <see cref="ContactUs"/></returns>
        public ContactUs GetContactUsById(int contactUsId) 
        {
            try
            {
                // Garbage Collection
                ContactUs? contactUs;
                using (IContactUsDAL contactUsDAL = _unitOfWork.GetDAL<IContactUsDAL>()) 
                { 
                    contactUs= contactUsDAL.GetEntityById(contactUsId);
                }

                if (contactUs != null)
                {
                    _logger.LogInformation("ContactUs instance retrieved succesfully for contact us ID: {contactUsId}, at GetContactUsById.", contactUsId);
                    return contactUs;
                }
                else
                {
                    _logger.LogWarning("No ContactUs instance found for contact us ID: {contactUsId}, at GetContactUsById", contactUsId);
                    throw new CustomException("ContactUs instance retrieved at GetContactUsById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving ContactUs instance for contact us ID: {contactUsId}, at GetContactUsById: {errorMessage}", contactUsId, exception);
                throw new CustomException("Exception Caught at GetContactUsById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>ContactUs</c>.
        /// </summary>
        /// <returns>List of <see cref="ContactUs"/>? Instances</returns>
        public List<ContactUs>? GetAllContactUs() 
        {
            try
            {
                // Garbage Collections
                List<ContactUs> listOfContactUs;
                using (IContactUsDAL contactUsDAL = _unitOfWork.GetDAL<IContactUsDAL>())
                {
                    listOfContactUs = contactUsDAL.GetAllRecords().ToList();
                }

                if (listOfContactUs != null)
                {
                    _logger.LogInformation("All ContactUs instances retrieved successfully at GetAllContactUs.");
                }
                else
                {
                    _logger.LogInformation("No ContactUs instances found at GetAllContactUs.");
                }
                return listOfContactUs;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving ContactUs instances at GetAllContactUs: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllContactUs: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>ContactUs</c> to the Database. 
        /// </summary>
        /// <param name="contactUs"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="ContactUs"/> added successfully.</returns>
        public bool AddContactUs(ContactUs contactUs, int userId) 
        {
            try
            {
                if (contactUs != null)
                {
                    contactUs.Status = EntityStatus.Active;
                    contactUs.ModifiedBy = userId;
                    contactUs.ModifiedOn = DateTime.Now;

                    using (IContactUsDAL contactUsDAL = _unitOfWork.GetDAL<IContactUsDAL>()) 
                    { 
                        contactUsDAL.AddEntity(contactUs);
                    }

                    _unitOfWork.Commit();
                    _logger.LogInformation("New ContactUs instance successfully added at AddContactUs.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New ContactUs instance is null. Failed to add new ContactUs instance at AddContactUs.");
                    throw new CustomException("New ContactUs instance is null. Failed to add new ContactUs instance at AddContactUs.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new ContactUs instance at AddContactUs: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddContactUs: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of <c>ContactUs</c>.
        /// </summary>
        /// <param name="updatedContactUs"></param>
        /// <param name="userId"></param>
        /// <returns>True if the update operation was successfull, False otherwise.</returns>
        public bool UpdatedContactUs(ContactUs updatedContactUs, int userId) 
        {
            try
            {
                if (updatedContactUs != null)
                {

                    // Garbage Collection
                    using IContactUsDAL contactUsDAL = _unitOfWork.GetDAL<IContactUsDAL>();
                    ContactUs? existingContactUs = contactUsDAL.GetEntityById(updatedContactUs.Id);

                    if (existingContactUs != null)
                    {
                        existingContactUs.Email = !string.IsNullOrWhiteSpace(updatedContactUs.Email) ? updatedContactUs.Email : existingContactUs.Email;
                        existingContactUs.Subject = !string.IsNullOrWhiteSpace(updatedContactUs.Subject) ? updatedContactUs.Subject : existingContactUs.Subject;
                        existingContactUs.Message = !string.IsNullOrWhiteSpace(updatedContactUs.Message) ? updatedContactUs.Message : existingContactUs.Message;

                        existingContactUs.Status = updatedContactUs.Status;
                        existingContactUs.ModifiedBy = userId;
                        existingContactUs.ModifiedOn = DateTime.Now;

                        contactUsDAL.UpdateEntity(existingContactUs);

                        _unitOfWork.Commit();

                        _logger.LogInformation("ContactUs instance with ID: {contactUsId} updated successfully at UpdatedContactUs.", existingContactUs.Id);
                        return true;

                    }
                    else 
                    {
                        _logger.LogError("No ContactUs instance found for contact us ID: {contactUsId}, at UpdatedContactUs", updatedContactUs.Id);
                        throw new CustomException($"Existing ContactUs instance retrieved for contact us ID: {updatedContactUs.Id} is null at UpdatedContactUs.");
                    }
                }
                else
                {
                    _logger.LogWarning("Updated ContactUs instance passed is null. Failed to update ContactUs instance at UpdatedContactUs.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating ContactUs instance at UpdatedContactUs: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdatedContactUs: " + exception + ".", exception);
            }
        }
    }
}
