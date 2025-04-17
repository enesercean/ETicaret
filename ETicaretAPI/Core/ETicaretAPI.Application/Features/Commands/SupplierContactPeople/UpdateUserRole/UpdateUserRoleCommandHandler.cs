using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierContactPeople.UpdateUserRole
{
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommandRequest, UpdateUserRoleCommandResponse>
    {
        readonly IRoleService _roleService;

        public UpdateUserRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<UpdateUserRoleCommandResponse> Handle(UpdateUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Kullanıcının mevcut rollerini asenkron şekilde al
                var userRoleAudits = await _roleService.GetUserRoleAuditsByUserIdsAsync(new List<string> { request.Id.ToString() });
                var oldRole = userRoleAudits.FirstOrDefault();

                if (oldRole == null)
                {
                    // Kullanıcının rolü yoksa, direkt yeni rol ata
                    await _roleService.AssignRoleAsync(request.Id.ToString(), request.Role);
                    return new UpdateUserRoleCommandResponse
                    {
                        Success = true,
                        Message = $"Role '{request.Role}' assigned successfully."
                    };
                }

                // Aynı role atama yapılmak isteniyorsa işlem yapmaya gerek yok
                if (oldRole.RoleName == request.Role)
                {
                    return new UpdateUserRoleCommandResponse
                    {
                        Success = true,
                        Message = $"User already has the role '{request.Role}'."
                    };
                }

                // Rol değişikliği yap
                await _roleService.ChangeUserRoleAsync(
                    request.Id.ToString(),
                    oldRole.RoleName,
                    request.Role,
                    $"Role changed via UpdateUserRole command"
                );

                return new UpdateUserRoleCommandResponse
                {
                    Success = true,
                    Message = $"Role changed from '{oldRole.RoleName}' to '{request.Role}' successfully."
                };
            }
            catch (Exception ex)
            {
                return new UpdateUserRoleCommandResponse
                {
                    Success = false,
                    Message = $"Failed to update role: {ex.Message}"
                };
            }
        }
    }
}
