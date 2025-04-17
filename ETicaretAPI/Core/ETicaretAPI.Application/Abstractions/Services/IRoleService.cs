using ETicaretAPI.Application.DTOs.UserRoleAudit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        /// <summary>
        /// Assigns a role to a user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="roleName">Name of the role to assign</param>
        Task AssignRoleAsync(string userId, string roleName);

        /// <summary>
        /// Removes a role from a user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="roleName">Name of the role to remove</param>
        Task RemoveRoleAsync(string userId, string roleName);

        /// <summary>
        /// Checks if a user has a specific role
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="roleName">Name of the role to check</param>
        /// <returns>True if user has the role, false otherwise</returns>
        Task<bool> IsInRoleAsync(string userId, string roleName);

        /// <summary>
        /// Gets all roles assigned to a user
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <returns>Collection of role names assigned to the user</returns>
        Task<IList<string>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Creates a new role in the system
        /// </summary>
        /// <param name="roleName">Name of the role to create</param>
        Task CreateRoleAsync(string roleName);

        /// <summary>
        /// Deletes a role from the system
        /// </summary>
        /// <param name="roleName">Name of the role to delete</param>
        Task DeleteRoleAsync(string roleName);

        /// <summary>
        /// Gets all roles in the system
        /// </summary>
        /// <returns>List of all role names</returns>
        Task<List<string>> GetAllRolesAsync();

        /// <summary>
        /// Gets role assignments with update dates for a list of users
        /// </summary>
        /// <param name="userIds">List of user IDs</param>
        /// <returns>List of user role audits with user ID, role name and update date</returns>
        Task<List<ListUserRoleAudit>> GetUserRoleAuditsByUserIdsAsync(List<string> userIds);

        /// <summary>
        /// Changes a user's role from one role to another
        /// </summary>
        /// <param name="userId">ID of the user</param>
        /// <param name="oldRoleName">Current role name to be removed</param>
        /// <param name="newRoleName">New role name to be assigned</param>
        /// <param name="reason">Optional reason for the role change</param>
        /// <returns>Task representing the asynchronous operation</returns>
        Task ChangeUserRoleAsync(string userId, string oldRoleName, string newRoleName, string reason = null);
    }
}
