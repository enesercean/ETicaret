// Handler
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.ProductLike;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductLike.GetMostLikedProducts
{
    public class GetMostLikedProductsQueryHandler : IRequestHandler<GetMostLikedProductsQueryRequest, GetMostLikedProductsQueryResponse>
    {
        private readonly IProductLikeService _productLikeService;

        public GetMostLikedProductsQueryHandler(IProductLikeService productLikeService)
        {
            _productLikeService = productLikeService;
        }

        public async Task<GetMostLikedProductsQueryResponse> Handle(GetMostLikedProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productLikeService.GetMostLikedProductsAsync(request.Count);

            return new GetMostLikedProductsQueryResponse
            {
                Products = products
            };
        }
    }
}
