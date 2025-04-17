using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.ProductRating;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        private readonly IProductService _productService;

        public GetProductByIdQueryHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(Guid.Parse(request.Id));
            if (product == null)
            {
                return null;
            }

            return new GetProductByIdQueryResponse
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                BrandName = product.BrandName,
                SupplierName = product.SupplierName,
                Image = product.Image,
                Ratings = product.Ratings.Select(pr => new ListProductRating
                {
                    Id = pr.Id,
                    Comment = pr.Comment,
                    UserName = pr.UserName,
                    RatingValue = pr.RatingValue,
                    CreatedDate = pr.CreatedDate
                }).ToList()
            };
        }
    }
}
