using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IProductRatingService
    {
        Task<bool> AddRatingAsync(Guid productId, int rating, string review, string userId);
        Task UpdateRatingAsync(Guid ratingId, int rating, string review);
        Task DeleteRatingAsync(Guid ratingId);
        Task<(int rating, string review)> GetRatingAsync(Guid ratingId);
        Task<IEnumerable<(Guid ratingId, int rating, string review)>> GetRatingsForProductAsync(Guid productId);
    }
}
