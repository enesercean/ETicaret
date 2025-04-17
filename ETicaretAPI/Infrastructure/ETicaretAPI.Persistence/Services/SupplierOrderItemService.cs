using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Product;
using ETicaretAPI.Application.DTOs.ProductLike;
using ETicaretAPI.Application.Repositories.SupplierOrderItem;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class SupplierOrderItemService : ISupplierOrderItemService
    {
        readonly ISupplierOrderItemReadRepository _supplierOrderItemReadRepository;
        readonly ISupplierOrderItemWriteRepository _supplierOrderItemWriteRepository;
        readonly IConfiguration _configuration;

        public SupplierOrderItemService(ISupplierOrderItemReadRepository supplierOrderItemReadRepository, ISupplierOrderItemWriteRepository supplierOrderItemWriteRepository, IConfiguration configuration)
        {
            _supplierOrderItemReadRepository = supplierOrderItemReadRepository;
            _supplierOrderItemWriteRepository = supplierOrderItemWriteRepository;
            _configuration = configuration;
        }

        public async Task<List<MostSoldProduct>> GetMostSoldProductsAsync(int count)
        {
            var bestSellingItems = await _supplierOrderItemReadRepository.GetAll(tracking: false)
                .Include(i => i.Product)
                .ThenInclude(p => p.ProductImageFiles)
                .GroupBy(i => i.ProductId)
                .Select(g => new
                {
                    Product = g.First().Product,
                    TotalQuantity = g.Sum(i => i.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(count)
                .ToListAsync();

            var mostSoldProducts = bestSellingItems.Select(i => new MostSoldProduct
            {
                Id = i.Product.Id,
                Name = i.Product.Name,
                Price = i.Product.Price,
                Image = i.Product.ProductImageFiles.Any()
                    ? $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{i.Product.Id}/{i.Product.ProductImageFiles.First().FileName}"
                    : null,
                SupplierId = i.Product.SupplierId,
                Stock = i.Product.Stock,
            }).ToList();

            return mostSoldProducts;
        }


    }
}
