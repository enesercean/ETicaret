using ETicaretAPI.Application.Repositories.SupplierAddress;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.SupplierAddress
{
    public class SupplierAddressReadRepository : ReadRepository<Domain.Entities.SupplierAddress>, ISupplierAddressReadRepository
    {
        public SupplierAddressReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
