using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.DeleteProductImageFile
{
    public class DeleteProductImageFileCommandRequest : IRequest<DeleteProductImageFileCommandResponse>
    {
        public string ProductId { get; set; }
        public string ImageId { get; set; }
    }
}

