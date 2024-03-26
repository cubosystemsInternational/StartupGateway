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
    /// Business logic layer for managing operations related to model ChatDetailsBLL.
    /// </summary>
    public class ContactUsBLL
    {
        private readonly ILogger<ChatDetailsBLL> logger;
        private readonly IUnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize ContactUsBLL with necessary dependencies.
        /// </summary>
        /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public ContactUsBLL(ILogger<ChatDetailsBLL> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the GetContactUsById instance information for the id passed.
        /// </summary>
        /// <param name="contactUsId"></param>
        /// <returns>ContactUs</returns>
        public ContactUs GetContactUsById(int contactUsId) 
        {
            try
            {
                var contactUs = unitOfWork.GetDAL<IContactUsDAL>().GetEntityById(contactUsId);
                if (contactUs != null)
                {
                    logger.LogInformation("ContactUs instance retrieved succesfully at GetContactUsById.");
                    return contactUs;
                }
                else
                {
                    logger.LogInformation("ContactUs instance retrieved at GetContactUsById is null.");
                    throw new CustomException("ContactUs instance retrieved at GetContactUsById is null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetContactUsById: " + exception + ".");
                throw new CustomException("Exception Caught at GetContactUsById: " + exception + ".");
            }
        }

        /// <summary>
        /// Retrieves all instances of ContactUs.
        /// </summary>
        /// <returns>List of ContactUs</returns>
        public List<ContactUs> GetAllContactUs() 
        {
            try
            {
                var listOfContactUs = unitOfWork.GetDAL<IContactUsDAL>().GetAllRecords().ToList();
                if (listOfContactUs != null)
                {
                    logger.LogInformation("ContactUs instances retrieved successfully at GetAllContactUs.");
                    return listOfContactUs;
                }
                else
                {
                    logger.LogInformation("Instances of ContactUs retrieved is null at GetAllContactUs.");
                    throw new CustomException("Instances of ContactUs retrieved is null at GetAllContactUs.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllContactUs: " + exception + ".");
                throw new CustomException("Exception Caught at GetAllContactUs: " + exception + ".");
            }
        }

        /// <summary>
        /// Adds an instance of ContactUs to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="contactUs"></param>
        /// <param name="userId"></param>
        /// <returns>True or False</returns>
        public bool AddContactUs(ContactUs contactUs, int userId) 
        {
            try
            {
                if (contactUs != null)
                {
                    contactUs.Status = EntityStatus.Active;
                    contactUs.ModifiedBy = userId;
                    contactUs.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IContactUsDAL>().AddEntity(contactUs);
                    unitOfWork.Commit();
                    logger.LogInformation("ContactUs instance successfully added at AddContactUs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ContactUs instance is null at AddContactUs.");
                    throw new CustomException("ContactUs instance is null at AddContactUs.");
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddContactUs: " + exception + ".");
                throw new CustomException("Exception Caught at AddContactUs: " + exception + ".");
            }
        }

        /// <summary>
        /// Updates an existing instance of ContactUs. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newContactUs"></param>
        /// <param name="userId"></param>
        /// <returns>True of False</returns>
        public bool UpdatedContactUs(ContactUs newContactUs, int userId) 
        {
            try
            {
                if (newContactUs != null)
                {
                    ContactUs existingChatDetails = unitOfWork.GetDAL<IContactUsDAL>().GetEntityById(newContactUs.Id);

                   
                    existingChatDetails.Status = newContactUs.Status;
                    existingChatDetails.ModifiedBy = userId;
                    existingChatDetails.ModifiedOn = DateTime.Now;

                    unitOfWork.GetDAL<IContactUsDAL>().UpdateEntity(existingChatDetails);
                    unitOfWork.Commit();

                    logger.LogInformation("ContactUs instance updated successfully at UpdatedContactUs.");
                    return true;
                }
                else
                {
                    logger.LogInformation("ContactUs instance passed at UpdatedContactUs are null.");
                    throw new CustomException("ContactUs instance passed at UpdatedContactUs are null.");
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdatedContactUs: " + exception + ".");
                throw new CustomException("Exception Caught at UpdatedContactUs: " + exception + ".");
            }
        }
    }
}
