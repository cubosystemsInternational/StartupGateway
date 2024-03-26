/**
 * Created by: Ibrahim
 * Created on: 21/03/2024
 * Description: Business logic class for RoleBLL .
 * 
 * */

using Microsoft.Extensions.Logging;
using StartupGateway.BusinessEntities;
using StartupGateway.DAL.Interfaces;
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
    /// Business logic layer for managing operations related to the Role entity.
    /// </summary>
    public class RolesBLL
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RolesBLL> _logger;

        /// <summary>
        /// Constructor to initialize RoleBLL with necessary dependencies.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="logger">The logger.</param>
        public RolesBLL(IUnitOfWork unitOfWork, ILogger<RolesBLL> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the Role specific details for the specified RoleId.
        /// </summary>
        /// <param name="roleId">The RoleId.</param>
        /// <returns>The Role entity.</returns>
        public Roles GetRoleById(int roleId)
        {
            try
            {
                using var roleRepository = _unitOfWork.GetDAL<IRolesDAL>();
                return roleRepository.GetEntityById(roleId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving role by ID: {RoleId}", roleId);
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        /// <summary>
        /// Retrieves all Roles.
        /// </summary>
        /// <returns>List of Role entities.</returns>
        public List<Roles> GetAllRoles()
        {
            try
            {
                _logger.LogInformation("In GetAllRoles");
                using var roleRepository = _unitOfWork.GetDAL<IRolesDAL>();
                return roleRepository.GetAllRecords().ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all roles.");
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        /// <summary>
        /// Adds a new Role.
        /// </summary>
        /// <param name="role">The Role entity to add.</param>
        /// <returns>True if the Role is added successfully, otherwise false.</returns>
        public bool AddRole(Roles role)
        {
            try
            {
                using var roleRepository = _unitOfWork.GetDAL<IRolesDAL>();
                role.Status = EntityStatus.Active;
                role.ModifiedOn = DateTime.Now;
                roleRepository.AddEntity(role);
                _unitOfWork.Commit();
                _logger.LogInformation("Role added successfully: {RoleName}.", role.RoleName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding role: {RoleName}.", role.RoleName);
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        /// <summary>
        /// Updates an existing Role.
        /// </summary>
        /// <param name="updatedRole">The updated Role entity.</param>
        /// <returns>The updated Role entity.</returns>
        public object UpdateRole(Roles updatedRole)
        {
            try
            {
                using var roleRepository = _unitOfWork.GetDAL<IRolesDAL>();
                var existingRole = roleRepository.GetEntityById(updatedRole.Id);

                if (existingRole != null)
                {
                    existingRole.RoleName = updatedRole.RoleName ?? existingRole.RoleName;
                    existingRole.Description = updatedRole.Description ?? existingRole.Description;
                    existingRole.Status = updatedRole.Status;
                    existingRole.ModifiedBy = updatedRole.ModifiedBy;
                    existingRole.ModifiedOn = DateTime.Now; // Update modified date

                    roleRepository.UpdateEntity(existingRole);
                    _unitOfWork.Commit();
                    return existingRole;
                }
                 
                throw new Exception("Role not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating role with ID: {RoleId}.", updatedRole.Id);
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}
