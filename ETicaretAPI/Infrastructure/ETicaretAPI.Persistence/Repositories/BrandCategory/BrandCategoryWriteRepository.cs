using ETicaretAPI.Application.Repositories.BrandCategory;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.BrandCategory
{
    public class BrandCategoryWriteRepository : WriteRepository<E.BrandCategory>, IBrandCategoryWriteRepository
    {
        public BrandCategoryWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
