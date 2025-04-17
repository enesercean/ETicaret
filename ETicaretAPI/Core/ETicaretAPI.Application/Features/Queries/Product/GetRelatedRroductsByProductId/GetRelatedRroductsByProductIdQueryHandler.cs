using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetRelatedRroductsByProductId
{
    public class GetRelatedProductsByProductIdQueryHandler : IRequestHandler<GetRelatedRroductsByProductIdQueryRequest, GetRelatedRroductsByProductIdQueryResponse>
    {
        readonly IProductService _productService;

        public GetRelatedProductsByProductIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetRelatedRroductsByProductIdQueryResponse> Handle(GetRelatedRroductsByProductIdQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetRelatedProductsByProductIdAsync(request.Id);
            
            return new() { listProducts = products };
        }
    }
}
