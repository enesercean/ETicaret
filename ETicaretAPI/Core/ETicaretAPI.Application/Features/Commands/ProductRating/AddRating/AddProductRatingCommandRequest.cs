using MediatR;
using System;

namespace ETicaretAPI.Application.Features.Commands.ProductRating.AddRating
{
    public class AddProductRatingCommandRequest : IRequest<AddProductRatingCommandResponse>
    {
        public Guid ProductId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
