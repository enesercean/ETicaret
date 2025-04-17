using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.ProductLike;
using ETicaretAPI.Application.Repositories.ProductLike;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class ProductLikeService : IProductLikeService
    {
        readonly IProductLikeReadRepository _productLikeReadRepository;
        readonly IProductLikeWriteRepository _productLikeWriteRepository;
        readonly IConfiguration _configuration;

        public ProductLikeService(IProductLikeReadRepository productLikeReadRepository, IProductLikeWriteRepository productLikeWriteRepository, IConfiguration configuration)
        {
            _productLikeReadRepository = productLikeReadRepository;
            _productLikeWriteRepository = productLikeWriteRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Kullanıcının bir ürünü beğenmesini sağlar
        /// </summary>
        public async Task<bool> AddLikeAsync(string userId, Guid productId)
        {
            var existingLike = await _productLikeReadRepository.GetSingleAsync(
                pl => pl.UserId == userId && pl.ProductId == productId);

            if (existingLike != null)
                return false;

            var productLike = new ProductLike
            {
                UserId = userId,
                ProductId = productId
            };

            await _productLikeWriteRepository.AddAsync(productLike);
            await _productLikeWriteRepository.SaveAsync();
            return true;
        }

        /// <summary>
        /// Kullanıcının ürün beğenisini kaldırır
        /// </summary>
        public async Task<bool> RemoveLikeAsync(string userId, Guid productId)
        {
            var like = await _productLikeReadRepository.GetSingleAsync(
                pl => pl.UserId == userId && pl.ProductId == productId);

            if (like == null)
                return false;

            _productLikeWriteRepository.Remove(like);
            await _productLikeWriteRepository.SaveAsync();
            return true;
        }

        /// <summary>
        /// Kullanıcının beğendiği tüm ürünleri getirir
        /// </summary>
        public async Task<List<MostLikedProduct>> GetUserLikedProductsAsync(string userId, bool includeProductDetails = true)
        {
            IQueryable<ProductLike> query = _productLikeReadRepository.GetWhere(pl => pl.UserId == userId);

            if (includeProductDetails)
            {
                query = query.Include(pl => pl.Product)
                             .ThenInclude(p => p.ProductImageFiles);
            }

            return await query.Select(pl => new MostLikedProduct
            {
                Id = pl.Product.Id,
                Name = pl.Product.Name,
                Stock = pl.Product.Stock,
                Price = pl.Product.Price,
                LikeCount = pl.Product.ProductLikes.Count,
                Image = $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{pl.Product.Id}/{pl.Product.ProductImageFiles.First().FileName}"
            }).ToListAsync();
        }

        /// <summary>
        /// Ürünü beğenen kullanıcı sayısını getirir
        /// </summary>
        public async Task<int> GetProductLikeCountAsync(Guid productId)
        {
            return await _productLikeReadRepository.GetWhere(pl => pl.ProductId == productId)
                                                  .CountAsync();
        }

        /// <summary>
        /// Kullanıcının belirli bir ürünü beğenip beğenmediğini kontrol eder
        /// </summary>
        public async Task<bool> HasUserLikedProductAsync(string userId, Guid productId)
        {
            return await _productLikeReadRepository.GetWhere(pl => pl.UserId == userId && pl.ProductId == productId)
                                                  .AnyAsync();
        }

        /// <summary>
        /// En çok beğenilen ürünleri getirir
        /// </summary>
        public async Task<List<MostLikedProduct>> GetMostLikedProductsAsync(int count = 10)
        {
            var mostLikedProducts = await _productLikeReadRepository.GetAll()
                .Include(pl => pl.Product)
                .ThenInclude(p => p.ProductImageFiles)
                .GroupBy(pl => pl.ProductId)
                .Select(group => new MostLikedProduct
                {
                    Id = group.First().Product.Id,
                    Name = group.First().Product.Name,
                    Stock = group.First().Product.Stock,
                    Price = group.First().Product.Price,
                    LikeCount = group.Count(),
                    Image = $"{_configuration["BaseUrl:Url"]}/Products/GetProductImage/{group.First().Product.Id}/{group.First().Product.ProductImageFiles.First().FileName ?? "a"}"
                })
                .OrderByDescending(p => p.LikeCount)
                .Take(count)
                .ToListAsync();

            return mostLikedProducts;
        }
    }
}
