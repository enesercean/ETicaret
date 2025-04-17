using ETicaretAPI.Application.Repositories.Brand;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.Brand
{
    public class BrandWriteRepository : WriteRepository<E.Brand>, IBrandWriteRepository
    {
        public BrandWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
