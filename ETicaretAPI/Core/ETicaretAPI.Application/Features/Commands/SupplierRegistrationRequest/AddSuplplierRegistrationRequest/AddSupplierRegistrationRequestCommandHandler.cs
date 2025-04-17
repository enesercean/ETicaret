using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.SupplierRegistrationRequest;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.SupplierRegistrationRequest.AddSuplplierRegistrationRequest
{
    public class AddSupplierRegistrationRequestCommandHandler : IRequestHandler<AddSupplierRegistrationRequestCommandRequest, AddSupplierRegistrationRequestCommandResponse>
    {
        readonly ISupplierRegistrationRequestService _supplierRegistrationRequestService;
        readonly IUserService _userService;

        public AddSupplierRegistrationRequestCommandHandler(ISupplierRegistrationRequestService supplierRegistrationRequestService, IUserService userService)
        {
            _supplierRegistrationRequestService = supplierRegistrationRequestService;
            _userService = userService;
        }

        public async Task<AddSupplierRegistrationRequestCommandResponse> Handle(AddSupplierRegistrationRequestCommandRequest request, CancellationToken cancellationToken)
        {
            string userId = await _userService.GetCurrentUserId();

            request.UserId = userId;

            CreateSupplierRegistrationRequest createSupplierRegistrationRequest = new CreateSupplierRegistrationRequest
            {
                UserId = request.UserId,
                BusinessName = request.BusinessName,
                BusinessType = request.BusinessType,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                TaxId = request.TaxId,
                BusinessAddress = request.BusinessAddress,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country,
                ProductCategories = request.ProductCategories,
                ProductDescription = request.ProductDescription,
                AveragePrice = request.AveragePrice,
                ShippingMethod = request.ShippingMethod
            };

            await _supplierRegistrationRequestService.CreateSupplierRegistrationRequest(createSupplierRegistrationRequest);

            return new();
        }
    }
}
