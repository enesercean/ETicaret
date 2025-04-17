using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.SupplierAddress;
using ETicaretAPI.Application.Repositories.Supplier;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories.Supplier
{
    public class SupplierWriteRepository : WriteRepository<E.Supplier>, ISupplierWriteRepository
    {
        public SupplierWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
