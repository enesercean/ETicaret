using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Supplier;
using ETicaretAPI.Application.DTOs.SupplierAddress;
using ETicaretAPI.Application.DTOs.SupplierContactPeople;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierRegistrationRequest.ApproveSupplierRegistrationRequest
{
    public class ApproveSupplierRegistrationRequestCommandHandler : IRequestHandler<ApproveSupplierRegistrationRequestCommandRequest, ApproveSupplierRegistrationRequestCommandResponse>
    {
        readonly ISupplierRegistrationRequestService _supplierRegistrationRequestService;
        readonly ISupplierService _supplierService;
        readonly ISupplierContactPeopleService _supplierContactPeopleService;
        readonly IRoleService _roleService;

        public ApproveSupplierRegistrationRequestCommandHandler(ISupplierRegistrationRequestService supplierRegistrationRequestService, ISupplierService supplierService, ISupplierContactPeopleService supplierContactPeopleService, IRoleService roleService)
        {
            _supplierRegistrationRequestService = supplierRegistrationRequestService;
            _supplierService = supplierService;
            _supplierContactPeopleService = supplierContactPeopleService;
            _roleService = roleService;
        }

        public async Task<ApproveSupplierRegistrationRequestCommandResponse> Handle(ApproveSupplierRegistrationRequestCommandRequest request, CancellationToken cancellationToken)
        {
            var supplierRequest = await _supplierRegistrationRequestService.ApproveSupplierRegistrationRequest(Guid.Parse(request.Id), request.Status, request.RejectionReason);

            CreateSupplier createSupplier = new CreateSupplier()
            {
                Id = new Guid(request.Id),
                Name = supplierRequest.BusinessName,
                Address = supplierRequest.BusinessAddress,
                City = supplierRequest.City,
                Country = supplierRequest.Country,
                PhoneNumber = supplierRequest.PhoneNumber,
                Position = "SupplierManager",
                PostalCode = supplierRequest.PostalCode
            };

            await _supplierService.AddSupplierAsync(createSupplier);

            CreateSupplierContactPeople createSupplierContactPeople = new CreateSupplierContactPeople()
            {
                PhoneNumber = supplierRequest.PhoneNumber,
                UserId = Guid.Parse(supplierRequest.UserId),
                SupplierId = createSupplier.Id
            };

            await _supplierContactPeopleService.AddSupplierContactPeopleAsync(createSupplierContactPeople);

            await _roleService.AssignRoleAsync(supplierRequest.UserId, Roles.SupplierManager);

            return new();
        }
    }
}
