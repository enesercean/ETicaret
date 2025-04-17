using ETicaretAPI.Application.DTOs.ProductLike;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IProductLikeService
    {
        /// <summary>
        /// Kullanıcının bir ürünü beğenmesini sağlar
        /// </summary>
        Task<bool> AddLikeAsync(string userId, Guid productId);

        /// <summary>
        /// Kullanıcının ürün beğenisini kaldırır
        /// </summary>
        Task<bool> RemoveLikeAsync(string userId, Guid productId);

        /// <summary>
        /// Kullanıcının beğendiği tüm ürünleri getirir
        /// </summary>
        Task<List<MostLikedProduct>> GetUserLikedProductsAsync(string userId, bool includeProductDetails = true);

        /// <summary>
        /// Ürünü beğenen kullanıcı sayısını getirir
        /// </summary>
        Task<int> GetProductLikeCountAsync(Guid productId);

        /// <summary>
        /// Kullanıcının belirli bir ürünü beğenip beğenmediğini kontrol eder
        /// </summary>
        Task<bool> HasUserLikedProductAsync(string userId, Guid productId);

        /// <summary>
        /// En çok beğenilen ürünleri getirir
        /// </summary>
        Task<List<MostLikedProduct>> GetMostLikedProductsAsync(int count = 10);
    }
}
