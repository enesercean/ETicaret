using ETicaretAPI.Application.DTOs.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IBrandService
    {
        Task<List<ListBrand>> GetBrandByCategories(List<string> categoryIdList);
        Task<Guid> CreateBrandAsync(string name, List<Guid>? categoryIds = null);
        Task<List<ListBrandWithCategories>> GetAllBrandsAsync();
    }
}
