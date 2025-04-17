using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Application.Repositories.ProductRepositories;
using E=ETicaretAPI.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories.ProductCategory;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories.Brand;
using ETicaretAPI.Application.Abstractions.Services;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        readonly IProductService _productService;
        readonly IProductHubService _productHubService;
        readonly IUserService _userService;
        readonly ISupplierService _supplierService;

        public CreateProductCommandHandler(IProductService productService, IProductHubService productHubService, IUserService userService, ISupplierService supplierService)
        {
            _productService = productService;
            _productHubService = productHubService;
            _userService = userService;
            _supplierService = supplierService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserId();

            if (userId == null)
            {
                throw new Exception("Unauthorized");
            }

            var supplier = await _supplierService.
                GetSupplierAsync(userId?.ToString());

            await _productService.CreateProductAsync(new()
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock,
                BrandId = request.BrandId,
                SupplierId = supplier.SupplierId, // supplierId servisten alınd !!!!
                CategoryIds = request?.CategoryIds ?? null
            });

            await _productHubService.ProductAddedMessageAsync($"{request?.Name} Product added successfully");
            return new();
        }
    }
}
