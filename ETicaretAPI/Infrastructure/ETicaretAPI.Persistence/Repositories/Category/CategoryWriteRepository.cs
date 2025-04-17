using E=ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.Category
{
    public class CategoryWriteRepository : WriteRepository<E.Category>, ICategoryWriteRepository
    {
        public CategoryWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
