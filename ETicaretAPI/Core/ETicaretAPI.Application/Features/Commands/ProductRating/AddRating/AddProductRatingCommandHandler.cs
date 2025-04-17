using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.ProductRating.AddRating
{
    public class AddProductRatingCommandHandler : IRequestHandler<AddProductRatingCommandRequest, AddProductRatingCommandResponse>
    {
        private readonly IProductRatingService _productRatingService;
        private readonly IUserService _userService;

        public AddProductRatingCommandHandler(IProductRatingService productRatingService, IUserService userService)
        {
            _productRatingService = productRatingService;
            _userService = userService;
        }

        public async Task<AddProductRatingCommandResponse> Handle(AddProductRatingCommandRequest request, CancellationToken cancellationToken)
        {
            string userId = await _userService.GetCurrentUserId();

            bool result = await _productRatingService.AddRatingAsync(request.ProductId, request.Rating, request.Review, userId);

            return new AddProductRatingCommandResponse
            {
                Success = result,
                Message = "Rating added successfully"
            };
        }
    }
}
