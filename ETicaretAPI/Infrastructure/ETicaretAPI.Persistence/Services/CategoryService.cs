using ETicaretAPI.Application.Abstractions.Services.Authentications;
using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Services
{
    public class CategoryService : ICategoryService
    {
        readonly ICategoryReadRepository _categoryReadRepository;
        readonly ICategoryWriteRepository _categoryWriteRepository;

        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            var categories = await _categoryReadRepository.GetAll(false).ToListAsync();
            return categories;
        }

        public async Task<Category> AddCategoryAsync(string name, Guid? parentCategoryId = null)
        {
            if(parentCategoryId.HasValue)
            {
                var parentCategory = await _categoryReadRepository.GetByIdAsync(parentCategoryId.Value.ToString());
                if(parentCategory == null)
                    throw new Exception($"Parent category with id {parentCategoryId} not found");
            }

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                ParentCategoryId = parentCategoryId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _categoryWriteRepository.AddAsync(category);
            await _categoryWriteRepository.SaveAsync();

            return category;
        }
    }
}
