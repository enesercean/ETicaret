using ETicaretAPI.Application.Repositories.SupplierOrderItem;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.SupplierOrderItem
{
    public class SupplierOrderItemReadRepository : ReadRepository<E.SupplierOrderItem>, ISupplierOrderItemReadRepository
    {
        public SupplierOrderItemReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
