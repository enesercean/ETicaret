using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Supplier.GetSupplierByUser
{
    public class GetSupplierByUserQueryHandler : IRequestHandler<GetSupplierByUserQueryRequest, GetSupplierByUserQueryResponse>
    {
        readonly ISupplierService _supplierService;
        readonly IHttpContextAccessor _httpContextAccessor;

        public GetSupplierByUserQueryHandler(ISupplierService supplierService, IHttpContextAccessor httpContextAccessor)
        {
            _supplierService = supplierService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<GetSupplierByUserQueryResponse> Handle(GetSupplierByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not found");
            }

            var supplier = await _supplierService.GetSupplierAsync(userId);

            return new()
            {
                SupplierId = supplier.SupplierId,
                Name = supplier.Name
            };
        }
    }
}
