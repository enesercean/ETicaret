using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductService _productService;

        public GetAllProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var productList = await _productService.GetAllProductAsync();

            return new() { Products = productList };
        }
    }
}
