using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Infrastructure.Constants;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ETicaretAPI.Application.Features.Commands.SupplierContactPeople.CreateSupplierContactPeople
{
    public class CreateSupplierContactPeopleCommandHandler : IRequestHandler<CreateSupplierContactPeopleCommandRequest, CreateSupplierContactPeopleCommandResponse>
    {
        readonly ISupplierContactPeopleService _supplierContactPeopleService;
        readonly IRoleService _roleService;
        readonly ISupplierService _supplierService;
        readonly IUserService _userService;

        public CreateSupplierContactPeopleCommandHandler(ISupplierContactPeopleService supplierContactPeopleService, IRoleService roleService, ISupplierService supplierService, IUserService userService)
        {
            _supplierContactPeopleService = supplierContactPeopleService;
            _roleService = roleService;
            _supplierService = supplierService;
            _userService = userService;
        }

        public async Task<CreateSupplierContactPeopleCommandResponse> Handle(CreateSupplierContactPeopleCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Get the current user ID (this is likely the supplier manager/admin)
                var currentUserId = await _userService.GetCurrentUserId();

                // Get the supplier for the current user
                var supplier = await _supplierService.GetSupplierAsync(currentUserId);

                // Create the supplier contact person entry
                var scp = new DTOs.SupplierContactPeople.CreateSupplierContactPeople
                {
                    UserId = request.UserId,
                    SupplierId = supplier.SupplierId,
                    PhoneNumber = "" // You might want to add this to the request or get it from the user
                };

                // Add the supplier contact person
                await _supplierContactPeopleService.AddSupplierContactPeopleAsync(scp);

                // Determine which role to assign based on request.Role
                string roleToAssign;

                if (!string.IsNullOrEmpty(request.Role) && request.Role == Roles.Employee)
                {
                    // If "Write" is in the role name, assign Employee role (with write permissions)
                    roleToAssign = Roles.Employee;
                }
                else
                {
                    // Otherwise, assign EmployeeRead role (read-only permissions)
                    roleToAssign = Roles.EmployeeRead;
                }

                // Assign the determined role
                await _roleService.AssignRoleAsync(request.UserId.ToString(), roleToAssign);

                return new CreateSupplierContactPeopleCommandResponse
                {
                    Success = true,
                    Message = $"User successfully added as supplier contact with {roleToAssign} role"
                };
            }
            catch (Exception ex)
            {
                return new CreateSupplierContactPeopleCommandResponse
                {
                    Success = false,
                    Message = $"Failed to create supplier contact: {ex.Message}"
                };
            }
        }
    }
}
