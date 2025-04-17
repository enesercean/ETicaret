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
    public class SupplierOrderItemWriteRepository : WriteRepository<E.SupplierOrderItem>, ISupplierOrderItemWriteRepository
    {
        public SupplierOrderItemWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
