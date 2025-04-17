using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductWithCategory
{
    public class GetProductWithCategoryHandler : IRequestHandler<GetProductWithCategoryRequest, GetProductWithCategoryResponse>
    {
        readonly IProductService _productService;
        readonly IConfiguration _configuration;

        public GetProductWithCategoryHandler(IProductService productService, IConfiguration configuration)
        {
            _productService = productService;
            _configuration = configuration;
        }

        public async Task<GetProductWithCategoryResponse> Handle(GetProductWithCategoryRequest request, CancellationToken cancellationToken)
        {
            List<Guid> categoryGuidList = new List<Guid>();

            if (request.CategoryIdList != null && request.CategoryIdList.Any())
            {
                foreach (var categoryId in request.CategoryIdList)
                {
                    if (Guid.TryParse(categoryId, out Guid guid))
                    {
                        categoryGuidList.Add(guid);
                    }
                    else
                    {
                        Console.WriteLine($"Geçersiz GUID formatı: {categoryId}");
                    }
                }
            }

            var productsByCategory = await _productService.GetProductsByCategoryIdAsync(categoryGuidList);

            var products = productsByCategory.Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                brandName = p.Brand != null ? p.Brand.Name : null,
                p.CreatedDate,
                p.UpdatedDate,
                image = $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{p.Id}/{p?.ProductImageFiles?.FirstOrDefault()?.FileName}"
            }).ToList();

            return new()
            {
                Products = products
            };

        }
    }
}
