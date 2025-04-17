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
    public class CategoryReadRepository : ReadRepository<E.Category>, ICategoryReadRepository
    {
        public CategoryReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
