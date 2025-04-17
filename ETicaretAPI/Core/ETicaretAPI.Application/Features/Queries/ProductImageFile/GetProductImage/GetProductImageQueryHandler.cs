using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductImage
{
    public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQueryRequest, GetProductImageQueryResponse>
    {
        private readonly IProductReadRepository _productReadRepository;

        public GetProductImageQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetProductImageQueryResponse> Handle(GetProductImageQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id), cancellationToken);

            if (product == null)
            {
                return null;
            }

            var productImages = product.ProductImageFiles.Select(p => new ProductImageDto
            {
                //todo: baseUrl should be read from appsettings.json
                Path = $"https://localhost:7081/api/Products/GetProductImage/{p.Id}/{p.FileName}",
                FileName = p.FileName,
                Id = p.Id.ToString()
            }).ToList();

            return new GetProductImageQueryResponse
            {
                ProductImages = productImages
            };
        }
    }
}
