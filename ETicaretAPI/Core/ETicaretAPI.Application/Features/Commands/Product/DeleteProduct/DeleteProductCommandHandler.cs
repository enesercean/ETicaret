using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _productWriteRepository.RemoveAsync(request.Id);
            if (!result)
            {
                return new DeleteProductCommandResponse
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            await _productWriteRepository.SaveAsync();

            return new DeleteProductCommandResponse
            {
                Success = true,
                Message = "Product deleted successfully"
            };
        }
    }
}
