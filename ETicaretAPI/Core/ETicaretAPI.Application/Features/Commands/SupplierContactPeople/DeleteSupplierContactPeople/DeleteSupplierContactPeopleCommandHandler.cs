using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierContactPeople.DeleteSupplierContactPeople
{
    public class DeleteSupplierContactPeopleCommandHandler : IRequestHandler<DeleteSupplierContactPeopleCommandRequest, DeleteSupplierContactPeopleCommandResponse>
    {
        readonly IRoleService _roleService;
        readonly ISupplierContactPeopleService _supplierContactPeopleService;

        public DeleteSupplierContactPeopleCommandHandler(IRoleService roleService, ISupplierContactPeopleService supplierContactPeopleService)
        {
            _roleService = roleService;
            _supplierContactPeopleService = supplierContactPeopleService;
        }

        public async Task<DeleteSupplierContactPeopleCommandResponse> Handle(DeleteSupplierContactPeopleCommandRequest request, CancellationToken cancellationToken)
        {
            var oldRole = _roleService.GetUserRoleAuditsByUserIdsAsync(new List<string> { request.Id.ToString()}).Result.FirstOrDefault();
            await _supplierContactPeopleService.DeleteSupplierContactPeopleAsync(request.Id);
            await _roleService.ChangeUserRoleAsync(request.Id.ToString(), oldRole?.RoleName, "Customer");



            return new DeleteSupplierContactPeopleCommandResponse
            {
                Success = true,
                Message = "User role changed successfully"
            };


        }
    }
}
