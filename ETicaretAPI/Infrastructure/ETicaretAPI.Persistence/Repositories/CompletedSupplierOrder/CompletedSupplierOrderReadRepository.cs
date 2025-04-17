using ETicaretAPI.Application.Repositories.CompletedSupplierOrder;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.CompletedSupplierOrder
{
    public class CompletedSupplierOrderReadRepository : ReadRepository<E.CompletedSupplierOrder>, ICompletedSupplierOrderReadRepository
    {
        public CompletedSupplierOrderReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
