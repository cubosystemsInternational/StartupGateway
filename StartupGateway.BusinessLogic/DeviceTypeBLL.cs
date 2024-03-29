﻿/**
 * Created by: Zuhri
 * Created on: 20/03/2024
 * Description: Business logic class DeviceTypeBLL created.
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
    /// Business logic layer for managing operations related to model DeviceTypeBLL.
    /// </summary>
    public class DeviceTypeBLL
    {
        private readonly ILogger<DeviceTypeBLL> logger;
        private readonly UnitOfWork unitOfWork;

        /// <summary>
        /// Constructor to intialize DeviceTypeBLL with necessary dependencies.
        /// </summary>
        public DeviceTypeBLL(ILogger<DeviceTypeBLL> logger, UnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the DeviceType information for the Id passed.
        /// </summary>
        /// <param name="chatDetailId"></param>
        /// <returns>DeviceType?</returns>
        public DeviceType? GetDeviceTypeById(int chatDetailId)
        {
            try
            {
                var DeviceType = unitOfWork.GetDAL<DeviceTypeDAL>().GetEntityById(chatDetailId);
                if (DeviceType != null)
                {
                    logger.LogInformation("DeviceType retrieved succesfully at GetDeviceTypeById.");
                    return DeviceType;
                }
                else
                {
                    logger.LogInformation("DeviceType retrieved at GetDeviceTypeById is null.");
                    return null;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetDeviceTypeById: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Retrieves all the DeviceType.
        /// </summary>
        /// <returns>List of DeviceType?</returns>
        public List<DeviceType>? GetAllDeviceType()
        {
            try
            {
                var listOfDeviceType = unitOfWork.GetDAL<DeviceTypeDAL>().GetAllRecords().ToList();
                logger.LogInformation("DeviceType retrieved successfully at GetAllDeviceType.");
                return listOfDeviceType;
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at GetAllDeviceType: " + exception + ".");
                return null;
            }
        }

        /// <summary>
        /// Adds an instance of DeviceType to the DataBase. Returns True if operation was successfull.
        /// </summary>
        /// <param name="DeviceType"></param>
        /// <returns>True or False</returns>
        public bool AddDeviceType(DeviceType DeviceType)
        {
            try
            {
                if (DeviceType != null)
                {
                    unitOfWork.GetDAL<DeviceTypeDAL>().AddEntity(DeviceType);
                    unitOfWork.Commit();
                    logger.LogInformation("DeviceType successfully added at AddDeviceType.");
                    return true;
                }
                else
                {
                    logger.LogInformation("DeviceType is null at AddDeviceType");
                    return false;
                }

            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at AddDeviceType: " + exception + ".");
                return false;
            }
        }

        /// <summary>
        /// Updates an existing instance of DeviceType. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="newDeviceType"></param>
        /// <returns>True or False</returns>
        public bool UpdateDeviceType(DeviceType newDeviceType, int userId)
        {
            try
            {
                if (newDeviceType != null)
                {
                    DeviceType existingDeviceType = unitOfWork.GetDAL<DeviceTypeDAL>().GetEntityById(newDeviceType.DeviceId);
                    existingDeviceType.DeviceId = newDeviceType.DeviceId;
                    existingDeviceType.DeviceName = newDeviceType.DeviceName;
                    existingDeviceType.DeviceModel = newDeviceType.DeviceModel;
                    existingDeviceType.Status = newDeviceType.Status;
                    existingDeviceType.ModifiedOn = DateTime.Now;
                    existingDeviceType.ModifiedBy = userId;

                    unitOfWork.Commit();

                    logger.LogInformation("DeviceType updated successfully at UpdateDeviceType.");
                    return true;
                }
                else
                {
                    logger.LogInformation("DeviceType passed at UpdateDeviceType are null.");
                    return false;
                }
            }
            catch (Exception exception)
            {
                logger.LogInformation("Exception Caught at UpdateDeviceType: " + exception + ".");
                return false;
            }
        }
    }
}
