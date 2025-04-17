// Application/ETicaretAPI.Application/Features/Queries/ProductLike/CheckUserLiked/CheckUserLikedQueryHandler.cs
using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.ProductLike.CheckUserLiked
{
    public class CheckUserLikedQueryHandler : IRequestHandler<CheckUserLikedQueryRequest, CheckUserLikedQueryResponse>
    {
        private readonly IProductLikeService _productLikeService;
        private readonly IUserService _userService;

        public CheckUserLikedQueryHandler(IProductLikeService productLikeService, IUserService userService)
        {
            _productLikeService = productLikeService;
            _userService = userService;
        }

        public async Task<CheckUserLikedQueryResponse> Handle(CheckUserLikedQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserId();
            
            if (string.IsNullOrEmpty(userId))
                return new CheckUserLikedQueryResponse { IsLiked = false };

            bool isLiked = await _productLikeService.HasUserLikedProductAsync(userId, request.ProductId);
            
            return new CheckUserLikedQueryResponse
            {
                IsLiked = isLiked
            };
        }
    }
}
