using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetMostSoldProduct
{
    public class GetMostSoldProductsQueryHandler : IRequestHandler<GetMostSoldProductsQueryRequest, GetMostSoldProductsQueryResponse>
    {
        readonly ISupplierOrderItemService _supplierOrderItemService;

        public GetMostSoldProductsQueryHandler(ISupplierOrderItemService supplierOrderItemService)
        {
            _supplierOrderItemService = supplierOrderItemService;
        }

        public async Task<GetMostSoldProductsQueryResponse> Handle(GetMostSoldProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var mostSoldProducts = await _supplierOrderItemService.GetMostSoldProductsAsync(request.Count);

            return new GetMostSoldProductsQueryResponse
            {
                Products = mostSoldProducts
            };
        }
    }
}
