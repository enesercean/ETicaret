using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Repositories.ProductRating;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class ProductRatingService : IProductRatingService
    {
        readonly IProductRatingReadRepository _productRatingReadRepository;
        readonly IProductRatingWriteRepository _productRatingWriteRepository;

        public ProductRatingService(IProductRatingReadRepository productRatingReadRepository, IProductRatingWriteRepository productRatingWriteRepository)
        {
            _productRatingReadRepository = productRatingReadRepository;
            _productRatingWriteRepository = productRatingWriteRepository;
        }

        public async Task<bool> AddRatingAsync(Guid productId, int rating, string review, string userId)
        {
            var comment = _productRatingReadRepository.GetWhere(r => r.ProductId == productId && r.UserId == userId).FirstOrDefault();

            if (comment != null)
            {
                return false; // Indicate that the rating was not added
            }

            var productRating = new ProductRating
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                RatingValue = rating,
                Comment = review,
                UserId = userId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _productRatingWriteRepository.AddAsync(productRating);
            await _productRatingWriteRepository.SaveAsync();

            return true;
        }

        public async Task DeleteRatingAsync(Guid ratingId)
        {
            var rating = await _productRatingReadRepository.GetByIdAsync(ratingId.ToString());
            if (rating != null)
            {
                _productRatingWriteRepository.Remove(rating);
                await _productRatingWriteRepository.SaveAsync();
            }
        }

        public async Task<(int rating, string review)> GetRatingAsync(Guid ratingId)
        {
            var rating = await _productRatingReadRepository.GetByIdAsync(ratingId.ToString());
            if (rating != null)
            {
                return (rating.RatingValue, rating.Comment);
            }
            return (0, null);
        }

        public async Task<IEnumerable<(Guid ratingId, int rating, string review)>> GetRatingsForProductAsync(Guid productId)
        {
            var ratings = await _productRatingReadRepository.GetWhere(r => r.ProductId == productId).ToListAsync();
            return ratings.Select(r => (r.Id, r.RatingValue, r.Comment));
        }

        public async Task UpdateRatingAsync(Guid ratingId, int rating, string review)
        {
            var existingRating = await _productRatingReadRepository.GetByIdAsync(ratingId.ToString());
            if (existingRating != null)
            {
                existingRating.RatingValue = rating;
                existingRating.Comment = review;
                existingRating.UpdatedDate = DateTime.UtcNow;

                _productRatingWriteRepository.Update(existingRating);
                await _productRatingWriteRepository.SaveAsync();
            }
        }
    }
}
