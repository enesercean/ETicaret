using ETicaretAPI.Application.Repositories.ProductRepositories;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetByIdAsync(request.Id);
            if (product == null)
            {
                return new UpdateProductCommandResponse
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            product.Name = request.Name;
            product.Stock = request.Stock;
            product.Price = request.Price;

            await _productWriteRepository.SaveAsync();

            return new UpdateProductCommandResponse
            {
                Success = true,
                Message = "Product updated successfully"
            };
        }
    }
}

