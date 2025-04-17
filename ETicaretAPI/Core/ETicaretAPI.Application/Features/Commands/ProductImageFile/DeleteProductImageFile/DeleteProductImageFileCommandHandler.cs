using ETicaretAPI.Application.Repositories.ProductRepositories;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace ETicaretAPI.Application.Features.Commands.Product.DeleteProductImageFile
{
    public class DeleteProductImageFileCommandHandler : IRequestHandler<DeleteProductImageFileCommandRequest, DeleteProductImageFileCommandResponse>
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DeleteProductImageFileCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<DeleteProductImageFileCommandResponse> Handle(DeleteProductImageFileCommandRequest request, CancellationToken cancellationToken)
        {
            ETicaretAPI.Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.ImageId));

            ProductImageFile? productImageFile = product?.ProductImageFiles?.FirstOrDefault(p => p.Id == Guid.Parse(request.ProductId));
            if (productImageFile != null)
            {
                product?.ProductImageFiles.Remove(productImageFile);
                await _productWriteRepository.SaveAsync();

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", productImageFile.Path);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
            await _productWriteRepository.SaveAsync();

            return new DeleteProductImageFileCommandResponse
            {
                Success = true,
                Message = "Image deleted successfully"
            };
        }
    }
}

