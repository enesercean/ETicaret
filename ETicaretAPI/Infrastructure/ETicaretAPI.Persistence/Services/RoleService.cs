using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.UserRoleAudit;
using ETicaretAPI.Application.Repositories.UserRoleAudit;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRoleAuditWriteRepository _userRoleAuditWriteRepository;
        private readonly IUserRoleAuditReadRepository _userRoleAuditReadRepository;

        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IUserRoleAuditReadRepository userRoleAuditReadRepository, IUserRoleAuditWriteRepository userRoleAuditWriteRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _userRoleAuditReadRepository = userRoleAuditReadRepository;
            _userRoleAuditWriteRepository = userRoleAuditWriteRepository;
        }

        public async Task AssignRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception($"User with ID {userId} not found.");

            if (!await _roleManager.RoleExistsAsync(roleName))
                throw new Exception($"Role {roleName} does not exist.");

            if (!await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    // Rol atamasını tarih bilgisiyle kaydet
                    await _userRoleAuditWriteRepository.AddAsync(new UserRoleAudit
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        RoleName = roleName,
                        CreatedDate = DateTime.UtcNow,
                        UpdatedDate = DateTime.UtcNow,
                        ActionType = AuditActionType.Added,
                        ExpiryDate = null,
                        Reason = "Role assigned",
                        RoleId = _roleManager.Roles.First(r => r.Name == roleName).Id,
                        IsActive = true
                    });
                    await _userRoleAuditWriteRepository.SaveAsync();
                }
                else
                {
                    throw new Exception($"Failed to assign role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        public async Task ChangeUserRoleAsync(string userId, string oldRoleName, string newRoleName, string reason = null)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception($"User with ID {userId} not found.");

            // Yeni rol kontrolü
            if (!await _roleManager.RoleExistsAsync(newRoleName))
                throw new Exception($"Role '{newRoleName}' does not exist.");

            // Kullanıcının mevcut tüm rollerini getir
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Customer rolü hariç diğer tüm rolleri kaldır
            var rolesToRemove = currentRoles.Where(r => r != "Customer" && r != newRoleName).ToList();

            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                    throw new Exception($"Failed to remove existing roles: {string.Join(", ", removeResult.Errors.Select(e => e.Description))}");

                // Kaldırılan rollerin audit kayıtlarını pasif yap
                foreach (var roleName in rolesToRemove)
                {
                    var oldAudit = await _userRoleAuditReadRepository.GetSingleAsync(a =>
                        a.UserId == userId && a.RoleName == roleName && a.IsActive);

                    if (oldAudit != null)
                    {
                        oldAudit.IsActive = false;
                        oldAudit.UpdatedDate = DateTime.UtcNow;
                        oldAudit.Reason = reason ?? $"Changed to {newRoleName}";
                        oldAudit.ActionType = AuditActionType.Removed;
                        _userRoleAuditWriteRepository.Update(oldAudit);
                        await _userRoleAuditWriteRepository.SaveAsync();
                    }
                }
            }

            // Kullanıcı yeni rolde değilse ekle
            if (!await _userManager.IsInRoleAsync(user, newRoleName))
            {
                var addResult = await _userManager.AddToRoleAsync(user, newRoleName);
                if (!addResult.Succeeded)
                    throw new Exception($"Failed to assign role: {string.Join(", ", addResult.Errors.Select(e => e.Description))}");

                // Yeni rol için audit kaydı oluştur
                await _userRoleAuditWriteRepository.AddAsync(new UserRoleAudit
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    RoleId = _roleManager.Roles.First(r => r.Name == newRoleName).Id,
                    RoleName = newRoleName,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    ActionType = AuditActionType.Added,
                    Reason = reason ?? $"Changed from {string.Join(", ", rolesToRemove)}",
                    IsActive = true
                });
                await _userRoleAuditWriteRepository.SaveAsync();
            }
        }


        public async Task CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var result = await _roleManager.CreateAsync(new AppRole { Name = roleName });
                if (!result.Succeeded)
                    throw new Exception($"Failed to create role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
        }

        public async Task DeleteRoleAsync(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName))
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                var result = await _roleManager.DeleteAsync(role);
                if (!result.Succeeded)
                    throw new Exception($"Failed to delete role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            else
            {
                throw new Exception($"Role {roleName} does not exist.");
            }
        }

        public async Task<List<string>> GetAllRolesAsync()
        {
            return await Task.FromResult(_roleManager.Roles.Select(r => r.Name).ToList());
        }

        public async Task<List<ListUserRoleAudit>> GetUserRoleAuditsByUserIdsAsync(List<string> userIds)
        {
            if (userIds == null || !userIds.Any())
                return new List<ListUserRoleAudit>();

            // Belirtilen kullanıcı ID'lerine ait rol audit kayıtlarını getir
            var userRoleAudits = await _userRoleAuditReadRepository.GetWhere(
                audit => userIds.Contains(audit.UserId) && audit.IsActive)
                .OrderByDescending(a => a.UpdatedDate)
                .ToListAsync();

            // Sonuçları DTO'ya dönüştür
            return userRoleAudits.Select(audit => new ListUserRoleAudit
            {
                UserId = audit.UserId,
                RoleName = audit.RoleName,
                UpdatedDate = audit.UpdatedDate
            }).ToList();
        }

        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception($"User with ID {userId} not found.");

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> IsInRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception($"User with ID {userId} not found.");

            return await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task RemoveRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new Exception($"User with ID {userId} not found.");

            if (await _userManager.IsInRoleAsync(user, roleName))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, roleName);
                if (result.Succeeded)
                {
                    // Audit kaydını sil veya kaldırıldı olarak işaretle
                    var audit = await _userRoleAuditReadRepository.GetSingleAsync(a => a.UserId == userId && a.RoleName == roleName);
                    if (audit != null)
                    {
                        _userRoleAuditWriteRepository.Remove(audit);
                        await _userRoleAuditWriteRepository.SaveAsync();
                    }
                }
                else
                {
                    throw new Exception($"Failed to remove role: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
