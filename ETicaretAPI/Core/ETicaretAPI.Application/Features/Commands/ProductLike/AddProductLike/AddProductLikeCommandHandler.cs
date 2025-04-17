// Application/ETicaretAPI.Application/Features/Commands/ProductLike/AddProductLike/AddProductLikeCommandHandler.cs
using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductLike.AddProductLike
{
    public class AddProductLikeCommandHandler : IRequestHandler<AddProductLikeCommandRequest, AddProductLikeCommandResponse>
    {
        private readonly IProductLikeService _productLikeService;
        private readonly IUserService _userService;

        public AddProductLikeCommandHandler(IProductLikeService productLikeService, IUserService userService)
        {
            _productLikeService = productLikeService;
            _userService = userService;
        }

        public async Task<AddProductLikeCommandResponse> Handle(AddProductLikeCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserId();
            
            if (string.IsNullOrEmpty(userId))
                return new AddProductLikeCommandResponse 
                { 
                    Succeeded = false, 
                    Message = "Kullanýcý bilgisi alýnamadý!" 
                };

            bool result = await _productLikeService.AddLikeAsync(userId, request.ProductId);
            
            return new AddProductLikeCommandResponse
            {
                Succeeded = result,
                Message = result ? "Ürün baþarýyla beðenildi." : "Bu ürün zaten beðenilmiþ."
            };
        }
    }
}
