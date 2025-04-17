// Application/ETicaretAPI.Application/Features/Commands/ProductLike/AddProductLike/AddProductLikeCommandRequest.cs
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.ProductLike.AddProductLike
{
    public class AddProductLikeCommandRequest : IRequest<AddProductLikeCommandResponse>
    {
        public Guid ProductId { get; set; }
        // UserId kullanýcýdan alýnmayacak, handler içinde alýnacak
    }
}
