using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.SupplierRegistrationRequest.GetSupplierRegistrationRequests
{
    public class GetSupplierRegistrationRequestsQueryHandler : IRequestHandler<GetSupplierRegistrationRequestsQueryRequest, List<GetSupplierRegistrationRequestsQueryResponse>>
    {
        private readonly ISupplierRegistrationRequestService _supplierRegistrationRequestService;

        public GetSupplierRegistrationRequestsQueryHandler(ISupplierRegistrationRequestService supplierRegistrationRequestService)
        {
            _supplierRegistrationRequestService = supplierRegistrationRequestService ?? throw new ArgumentNullException(nameof(supplierRegistrationRequestService));
        }

        public async Task<List<GetSupplierRegistrationRequestsQueryResponse>> Handle(GetSupplierRegistrationRequestsQueryRequest request, CancellationToken cancellationToken)
        {
            var supplierRegistrationList = await _supplierRegistrationRequestService.GetAllSupplierRegistrationRequests();

            return supplierRegistrationList.Select(srr => new GetSupplierRegistrationRequestsQueryResponse
            {
                Id = srr.Id,
                BusinessName = srr.BusinessName,
                BusinessType = srr.BusinessType,
                FirstName = srr.FirstName,
                LastName = srr.LastName,
                Email = srr.Email,
                PhoneNumber = srr.PhoneNumber,
                TaxNumber = srr.TaxNumber,
                BusinessAddress = srr.BusinessAddress,
                City = srr.City,
                Country = srr.Country,
                State = srr.State,
                PostalCode = srr.PostalCode,
                ProductCategories = srr.ProductCategories,
                ProductDescription = srr.ProductDescription,
                AvaragePrice = srr.AvaragePrice,
                ShippingMethod = srr.ShippingMethod,
                Status = srr.Status,
                RejectionReason = srr.RejectionReason,
                ReviewedDate = srr.ReviewedDate,
                CreatedDate = srr.CreatedDate,
                UpdatedDate = srr.UpdatedDate,
                IsTransfered = srr.IsTransfered
            }).ToList();
        }
    }
}
