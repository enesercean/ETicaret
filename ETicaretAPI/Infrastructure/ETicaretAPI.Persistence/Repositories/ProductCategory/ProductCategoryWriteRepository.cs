using E=ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories.ProductCategory;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.ProductCategory
{
    public class ProductCategoryWriteRepository : WriteRepository<E.ProductCategory>, IProductCategoryWriteRepository
    {
        public ProductCategoryWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
