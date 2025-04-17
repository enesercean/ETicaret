// Application/ETicaretAPI.Application/Features/Commands/ProductLike/AddProductLike/AddProductLikeCommandRequest.cs
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.ProductLike.AddProductLike
{
    public class AddProductLikeCommandRequest : IRequest<AddProductLikeCommandResponse>
    {
        public Guid ProductId { get; set; }
        // UserId kullan�c�dan al�nmayacak, handler i�inde al�nacak
    }
}
