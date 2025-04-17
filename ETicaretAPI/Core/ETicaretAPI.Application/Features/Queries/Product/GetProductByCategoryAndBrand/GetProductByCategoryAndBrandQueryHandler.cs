using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductByCategoryAndBrand
{
    public class GetProductByCategoryAndBrandQueryHandler : IRequestHandler<GetProductByCategoryAndBrandQueryRequest, GetProductByCategoryAndBrandQueryResponse>
    {
        readonly IProductService _productService;

        public GetProductByCategoryAndBrandQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetProductByCategoryAndBrandQueryResponse> Handle(GetProductByCategoryAndBrandQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsByBrandIdandCategoryAsync(request.BrandId, request.CategoryIds);

            return new GetProductByCategoryAndBrandQueryResponse
            {
                Products = products
            };
        }
    }
}
