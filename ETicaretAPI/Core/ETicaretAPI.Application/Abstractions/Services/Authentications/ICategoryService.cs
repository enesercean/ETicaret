using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services.Authentications
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> AddCategoryAsync(string name, Guid? parentCategoryId = null);
    }
}
