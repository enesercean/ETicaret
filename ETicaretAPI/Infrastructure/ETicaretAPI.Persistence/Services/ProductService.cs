using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Product;
using ETicaretAPI.Application.DTOs.ProductRating;
using ETicaretAPI.Application.Repositories.ProductRepositories;
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
    public class ProductService : IProductService
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IProductWriteRepository _productWriteRepository;
        readonly IConfiguration _configuration;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IConfiguration configuration)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _configuration = configuration;
        }

        public async Task CreateProductAsync(CreateProduct product)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                BrandId = product.BrandId,
                SupplierId = product.SupplierId,
                ProductCategories = product.CategoryIds.Select(c => new ProductCategory
                {
                    CategoryId = c,
                    ProductId = product.Id
                }).ToList()
            };

            await _productWriteRepository.AddAsync(newProduct);
            await _productWriteRepository.SaveAsync();
        }

        public async Task<List<ListProduct>> GetAllProductAsync()
        {
            var productList = await _productReadRepository.GetAll(false).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                BrandName = p.Brand != null ? p.Brand.Name : null,
                p.CreatedDate,
                p.UpdatedDate,
                image = $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{p.Id}/{p.ProductImageFiles.First().FileName}",
                p.SupplierId,
                SupplierName = p.Supplier.Name != null ? p.Supplier.Name : null,
            }).ToListAsync();

            var listProduct = productList.Select(p => new ListProduct
            {
                Id = p.Id,
                Name = p.Name,
                Stock = p.Stock,
                Price = p.Price,
                BrandName = p.BrandName ?? "null",
                SupplierId = p.SupplierId,
                SupplierName = p.SupplierName ?? "null",
                Image = p.image,
                CreatedDate = p.CreatedDate,

            }).ToList();

            return listProduct;
        }

        public async Task<List<ListProduct>> GetAllProductBySupplierAsync(string supplierId)
        {
            // Guid'e dönüştürme işlemi
            if (!Guid.TryParse(supplierId, out Guid supplierGuid))
                throw new Exception("Invalid supplier ID format");

            // Tedarikçiye ait ürünleri getir
            var products = await _productReadRepository.GetWhere(p => p.SupplierId == supplierGuid)
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImageFiles)
                .ToListAsync();

            // DTO'ya dönüştür
            var listProducts = products.Select(p => new ListProduct
            {
                Id = p.Id,
                Name = p.Name,
                Stock = p.Stock,
                Price = p.Price,
                BrandName = p.Brand?.Name ?? "null",
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.Name ?? "null",
                Image = p.ProductImageFiles.Any()
                    ? $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{p.Id}/{p.ProductImageFiles.First().FileName}"
                    : null
            }).ToList();

            return listProducts;
        }


        public async Task<ListProduct> GetProductByIdAsync(Guid id)
        {
            var product = await _productReadRepository.Table
                .Include(p => p.ProductImageFiles)
                .ThenInclude(pif => pif.Products)
                .ThenInclude(p => p.Brand)
                .Include(p => p.ProductRatings)
                .ThenInclude(pr => pr.User)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                throw new Exception("Product Not Found");

            var imageFile = product.ProductImageFiles.FirstOrDefault();
            string imagePath = imageFile != null
                ? $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{product.Id}/{imageFile.FileName}"
                : null;

            var listProduct = new ListProduct
            {
                Id = product.Id,
                Name = product.Name,
                Stock = product.Stock,
                Price = product.Price,
                BrandName = product.Brand?.Name ?? "null",
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier?.Name ?? "null",
                Image = imagePath,
                Ratings = product.ProductRatings.Select(r => new ListProductRating
                {
                    Id = r.Id,
                    UserName = r?.User?.UserName ?? "null",
                    RatingValue = r.RatingValue,
                    Comment = r.Comment ?? "null",
                    CreatedDate = r.CreatedDate
                }).ToList()
            };

            return listProduct;
        }


        public async Task<List<Product>> GetProductsByBrandIdandCategoryAsync(Guid brandId, List<Guid> categoryNames)
        {
            var products = await _productReadRepository.Table
                                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductImageFiles)
                    .ThenInclude(pif => pif.Products)
                    .ThenInclude(p => p.Brand)
                .Where(p => p.BrandId == brandId && p.ProductCategories.Any(pc => categoryNames.Contains(pc.CategoryId)))
                .ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(List<Guid> categoryIds)
        {
            var products = await _productReadRepository
                .GetWhere(p => p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryId)))
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductImageFiles)
                    .ThenInclude(pif => pif.Products)
                 .Include(p => p.Brand)
                .ToListAsync();
            return products;
        }

        public async Task<List<ListProduct>> GetRelatedProductsByProductIdAsync(Guid id)
        {
            // Fetch the product with its categories
            var product = await _productReadRepository.Table
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                throw new Exception("Product Not Found");

            // Get the category IDs of the product
            var categoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList();

            // Fetch related products that share the same categories
            var relatedProducts = await _productReadRepository.Table
                .Include(p => p.ProductCategories)
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImageFiles)
                .Where(p => p.Id != id && p.ProductCategories.Any(pc => categoryIds.Contains(pc.CategoryId)))
                .ToListAsync();

            // Shuffle the related products and take 5 random items
            var random = new Random();
            var randomRelatedProducts = relatedProducts.OrderBy(x => random.Next()).Take(5).ToList();

            // Map the related products to ListProduct DTO
            var listRelatedProducts = randomRelatedProducts.Select(p => new ListProduct
            {
                Id = p.Id,
                Name = p.Name,
                Stock = p.Stock,
                Price = p.Price,
                BrandName = p.Brand?.Name ?? "null",
                SupplierId = p.SupplierId,
                SupplierName = p.Supplier?.Name ?? "null",
                Image = p.ProductImageFiles.Any() ? $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{p.Id}/{p.ProductImageFiles.First().FileName}" : null
            }).ToList();

            return listRelatedProducts;
        }


    }
}
