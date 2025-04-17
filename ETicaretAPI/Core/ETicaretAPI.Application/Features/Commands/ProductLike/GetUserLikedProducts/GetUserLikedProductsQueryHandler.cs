using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.ProductLike;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductLike.GetUserLikedProducts
{
    public class GetUserLikedProductsQueryHandler : IRequestHandler<GetUserLikedProductsQueryRequest, GetUserLikedProductsQueryResponse>
    {
        private readonly IProductLikeService _productLikeService;
        private readonly IUserService _userService;
        readonly IConfiguration _configuration;

        public GetUserLikedProductsQueryHandler(IProductLikeService productLikeService, IUserService userService, IConfiguration configuration)
        {
            _productLikeService = productLikeService;
            _userService = userService;
            _configuration = configuration;
        }

        public async Task<GetUserLikedProductsQueryResponse> Handle(GetUserLikedProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
                return new GetUserLikedProductsQueryResponse { Products = new List<MostLikedProduct>() };

            List<MostLikedProduct> likedProducts = await _productLikeService.GetUserLikedProductsAsync(userId);

            if (likedProducts == null || !likedProducts.Any())
                return new GetUserLikedProductsQueryResponse { Products = new List<MostLikedProduct>() };

            return new GetUserLikedProductsQueryResponse
            {
                Products = likedProducts
            };
        }
    }
}
