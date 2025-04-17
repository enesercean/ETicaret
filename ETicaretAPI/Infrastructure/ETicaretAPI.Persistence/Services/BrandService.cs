using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Brand;
using ETicaretAPI.Application.DTOs.Category;
using ETicaretAPI.Application.Repositories.Brand;
using ETicaretAPI.Application.Repositories.BrandCategory;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    /// <summary>
    /// Implements brand-related business logic and operations
    /// </summary>
    public class BrandService : IBrandService
    {
        private readonly IBrandReadRepository _brandReadRepository;
        private readonly IBrandWriteRepository _brandWriteRepository;
        private readonly IBrandCategoryWriteRepository _brandCategoryWriteRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandService"/> class
        /// </summary>
        public BrandService(
            IBrandReadRepository brandReadRepository,
            IBrandWriteRepository brandWriteRepository,
            IBrandCategoryWriteRepository brandCategoryWriteRepository)
        {
            _brandReadRepository = brandReadRepository;
            _brandWriteRepository = brandWriteRepository;
            _brandCategoryWriteRepository = brandCategoryWriteRepository;
        }

        /// <summary>
        /// Retrieves brands associated with specified categories
        /// </summary>
        /// <param name="categoryIdList">List of category IDs as strings</param>
        /// <returns>A list of brands matching the specified categories</returns>
        public async Task<List<ListBrand>> GetBrandByCategories(List<string> categoryIdList)
        {
            if (categoryIdList == null || !categoryIdList.Any())
                return new List<ListBrand>();

            var brands = await _brandReadRepository.Table
                .Include(a => a.BrandCategories)
                .Where(x => x.BrandCategories.Any(bc => categoryIdList.Contains(bc.CategoryId.ToString())))
                .Select(b => new ListBrand
                {
                    Name = b.Name,
                    BrandId = b.Id
                })
                .ToListAsync();

            return brands;
        }

        /// <summary>
        /// Creates a new brand and optionally associates it with specified categories
        /// </summary>
        /// <param name="name">The brand name</param>
        /// <param name="categoryIds">Optional list of category IDs to associate with the brand</param>
        /// <returns>The ID of the created brand</returns>
        /// <exception cref="ArgumentException">Thrown when the brand name is empty or whitespace</exception>
        public async Task<Guid> CreateBrandAsync(string name, List<Guid>? categoryIds = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Brand name cannot be empty");

            var brand = new Brand
            {
                Id = Guid.NewGuid(),
                Name = name,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _brandWriteRepository.AddAsync(brand);
            await _brandWriteRepository.SaveAsync();

            if (categoryIds != null && categoryIds.Any())
            {
                var brandCategories = new List<BrandCategory>();
                foreach (var categoryId in categoryIds)
                {
                    brandCategories.Add(new BrandCategory
                    {
                        Id = Guid.NewGuid(),
                        BrandId = brand.Id,
                        CategoryId = categoryId,
                        CreatedDate = DateTime.UtcNow
                    });
                }

                await _brandCategoryWriteRepository.AddRangeAsync(brandCategories);
                await _brandCategoryWriteRepository.SaveAsync();
            }

            return brand.Id;
        }

        /// <summary>
        /// Retrieves all brands with their associated categories
        /// </summary>
        /// <returns>A list of brands with their categories</returns>
        public async Task<List<ListBrandWithCategories>> GetAllBrandsAsync()
        {
            var brands = await _brandReadRepository.Table
                .Include(b => b.BrandCategories)
                .ThenInclude(bc => bc.Category)
                .Select(b => new ListBrandWithCategories
                {
                    BrandId = b.Id,
                    Name = b.Name,
                    Categories = b.BrandCategories.Select(bc => new ListCategory
                    {
                        CategoryId = bc.CategoryId,
                        Name = bc.Category.Name
                    }).ToList()
                })
                .ToListAsync();

            return brands;
        }
    }
}
