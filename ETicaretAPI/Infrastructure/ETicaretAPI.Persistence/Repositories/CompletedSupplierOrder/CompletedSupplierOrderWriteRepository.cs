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
    public class CompletedSupplierOrderWriteRepository : WriteRepository<E.CompletedSupplierOrder>, ICompletedSupplierOrderWriteRepository
    {
        public CompletedSupplierOrderWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
