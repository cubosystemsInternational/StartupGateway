/**
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
using StartupGateway.Shared;
using StartupGateway.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StartupGateway.Shared.Share;

namespace StartupGateway.BusinessLogic
{
    /// <summary>
    /// Business logic layer for managing operations related to model <see cref="DeviceTypes"/>.
    /// </summary>
    public class DeviceTypesBLL
    {
        private readonly ILogger<DeviceTypesBLL> _logger;
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor to intialize DeviceTypesBLL with necessary dependencies.
        /// </summary>
        /// /// <param name="logger">Instance of Logger for logging information.</param>
        /// <param name="unitOfWork">Instance of Unit of Work.</param>
        public DeviceTypesBLL(ILogger<DeviceTypesBLL> logger, UnitOfWork unitOfWork)
        {
            this._logger = logger;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Retrieves the <c>DeviceTypes</c> instance information for the Id passed.
        /// </summary>
        /// <param name="deviceTypeId"></param>
        /// <returns>Instance of <see cref="DeviceTypes"/></returns>
        public DeviceTypes GetDeviceTypeById(int deviceTypeId)
        {
            try
            {
                // Grabage Collection
                DeviceTypes? deviceType;
                using (IDeviceTypesDAL deviceTypesDAL = _unitOfWork.GetDAL<IDeviceTypesDAL>())
                {
                    deviceType = deviceTypesDAL.GetEntityById(deviceTypeId);
                }

                if (deviceType != null)
                {
                    _logger.LogInformation("DeviceTypes instance retrieved succesfully for device type ID: {deviceTypeId}, at GetDeviceTypeById.", deviceTypeId);
                    return deviceType;
                }
                else
                {
                    _logger.LogWarning("No DeviceTypes instance found for device type ID: {deviceTypeId}, at GetDeviceTypeById", deviceTypeId);
                    throw new CustomException("DeviceTypes instance retrieved at GetDeviceTypeById is null.");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving DeviceTypes instance for device type ID: {deviceTypeId}, at GetDeviceTypeById: {errorMessage}", deviceTypeId, exception);
                throw new CustomException("Exception Caught at GetDeviceTypeById: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Retrieves all instances of <c>DeviceTypes</c>.
        /// </summary>
        /// <returns>List of <see cref="DeviceTypes"/>? Instances</returns>
        public List<DeviceTypes>? GetAllDeviceTypes()
        {
            try
            {
                // Garbage Collection
                List<DeviceTypes>? listOfDeviceTypes;
                using (IDeviceTypesDAL deviceTypesDAL = _unitOfWork.GetDAL<IDeviceTypesDAL>())
                {
                    listOfDeviceTypes = deviceTypesDAL.GetAllRecords().ToList();
                }

                if (listOfDeviceTypes != null)
                {
                    _logger.LogInformation("All DeviceTypes instances retrieved successfully at GetAllDeviceTypes.");
                }
                else 
                {
                    _logger.LogInformation("No DeviceTypes instances found at GetAllDeviceTypes.");
                }
                return listOfDeviceTypes;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error retrieving DeviceTypes instances at GetAllDeviceTypes: {errorMessage}", exception);
                throw new CustomException("Exception Caught at GetAllDeviceTypes: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Adds an instance of <c>DeviceTypes</c> to the Database.
        /// </summary>
        /// <param name="deviceType"></param>
        /// <param name="userId"></param>
        /// <returns>True if new instance of <see cref="DeviceTypes"/> added successfully.</returns>
        public bool AddDeviceType(DeviceTypes deviceType,int userId)
        {
            try
            {
                if (deviceType != null)
                {

                    deviceType.Status = EntityStatus.Active;
                    deviceType.ModifiedBy = userId;
                    deviceType.ModifiedOn = DateTime.Now;

                    // Garbage Collection
                    using (IDeviceTypesDAL deviceTypesDAL = _unitOfWork.GetDAL<IDeviceTypesDAL>()) 
                    {
                        deviceTypesDAL.AddEntity(deviceType);
                    }

                    _unitOfWork.Commit();
                    _logger.LogInformation("New DeviceTypes instance successfully added at AddDeviceType.");
                    return true;
                }
                else
                {
                    _logger.LogWarning("New DeviceTypes instance is null. Failed to add new DeviceTypes instance at AddDeviceType.");
                    throw new CustomException("New DeviceTypes instance is null. Failed to add new DeviceTypes instance at AddDeviceType.");
                }

            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error adding new DeviceTypes instance at AddDeviceType: {errorMessage}", exception);
                throw new CustomException("Exception Caught at AddDeviceType: " + exception + ".", exception);
            }
        }

        /// <summary>
        /// Updates an existing instance of DeviceType. Returns True, if the update operation was successfull.
        /// </summary>
        /// <param name="updatedDeviceType"></param>
        /// <returns>True or False</returns>
        public bool UpdateDeviceType(DeviceTypes updatedDeviceType, int userId)
        {
            try
            {
                if (updatedDeviceType != null)
                {
                    // Garbage Collection
                    using IDeviceTypesDAL deviceTypesDAL = _unitOfWork.GetDAL<IDeviceTypesDAL>();
                    DeviceTypes? existingDeviceType = deviceTypesDAL.GetEntityById(updatedDeviceType.Id);

                    if (existingDeviceType != null)
                    {
                        
                        existingDeviceType.DeviceName = !string.IsNullOrWhiteSpace(updatedDeviceType.DeviceName) ? updatedDeviceType.DeviceName: existingDeviceType.DeviceName;
                        existingDeviceType.DeviceModel = !string.IsNullOrWhiteSpace(updatedDeviceType.DeviceModel) ? updatedDeviceType.DeviceModel : existingDeviceType.DeviceModel;

                        existingDeviceType.Status = updatedDeviceType.Status;
                        existingDeviceType.ModifiedBy = userId;
                        existingDeviceType.ModifiedOn = DateTime.Now;

                        deviceTypesDAL.UpdateEntity(existingDeviceType);

                        _unitOfWork.Commit();

                        _logger.LogInformation("DeviceTypes instance with ID: {deviceTypeId} updated successfully at UpdateDeviceType.", existingDeviceType.Id);
                        return true;

                    }
                    else 
                    {
                        _logger.LogError("No DeviceTypes instance found for device type ID: {deviceTypeId}, at UpdateDeviceType", updatedDeviceType.Id);
                        throw new CustomException($"Existing DeviceTypes instance retrieved for device type ID: {updatedDeviceType.Id} is null at UpdateDeviceType.");
                    }
                    
                }
                else
                {
                    _logger.LogWarning("Updated DeviceTypes instance passed is null. Failed to update DeviceTypes instance at UpdateDeviceType.");
                    // We return False instead of an exception as it is very common to run update operations, without making any changes.
                    // Thus, this must not be treated as an exception.
                    return false;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error updating DeviceTypes instance at UpdateDeviceType: {errorMessage}", exception);
                throw new CustomException("Exception caught at UpdateDeviceType: " + exception + ".", exception);
            }
        }
    }
}
