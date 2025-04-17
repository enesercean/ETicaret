using ETicaretAPI.Application.Repositories.SupplierAddress;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.SupplierAddress
{
    public class SupplierAddressWriteRepository : WriteRepository<E.SupplierAddress>, ISupplierAddressWriteRepository
    {
        public SupplierAddressWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
