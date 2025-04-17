using ETicaretAPI.Application.Repositories.SupplierOrder;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.SupplierOrder
{
    public class SupplierOrderReadRepository : ReadRepository<E.SupplierOrder>, ISupplierOrderReadRepository
    {
        public SupplierOrderReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
