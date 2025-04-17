using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductLike.DeleteProduckLike
{
    public class DeleteProductLikeCommandHandler : IRequestHandler<DeleteProductLikeCommandRequest, DeleteProductLikeCommandResponse>
    {
        readonly IProductLikeService _productLikeService;
        readonly IUserService _userService;

        public DeleteProductLikeCommandHandler(IProductLikeService productLikeService, IUserService userService)
        {
            _productLikeService = productLikeService;
            _userService = userService;
        }

        public async Task<DeleteProductLikeCommandResponse> Handle(DeleteProductLikeCommandRequest request, CancellationToken cancellationToken)
        {
            string userId = await _userService.GetCurrentUserId();

            await _productLikeService.RemoveLikeAsync(userId, request.ProductId);

            return new DeleteProductLikeCommandResponse()
            {
                IsSuccess = true,
                Message = "Removed"
            };
        }
    }
}
