using ETicaretAPI.Application.Repositories.ProductCategory;
using E=ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.ProductCategory
{
    public class ProductCategoryReadRepository : ReadRepository<E.ProductCategory>, IProductCategoryReadRepository
    {
        public ProductCategoryReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
