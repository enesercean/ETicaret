using ETicaretAPI.Application.DTOs.Product;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsByCategoryIdAsync(List<Guid> categoryNames);
        Task<List<Product>> GetProductsByBrandIdandCategoryAsync(Guid brandId, List<Guid> categoryNames);
        Task<List<ListProduct>> GetAllProductAsync();
        Task<ListProduct> GetProductByIdAsync(Guid id);
        Task<List<ListProduct>> GetRelatedProductsByProductIdAsync(Guid id);
        Task CreateProductAsync(CreateProduct product);
        Task<List<ListProduct>> GetAllProductBySupplierAsync(string supplierId);
    }
}
