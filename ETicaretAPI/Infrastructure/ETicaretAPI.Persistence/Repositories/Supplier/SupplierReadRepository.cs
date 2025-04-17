using ETicaretAPI.Application.Repositories.Supplier;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Supplier
{
    public class SupplierReadRepository : ReadRepository<Domain.Entities.Supplier>, ISupplierReadRepository
    {
        public SupplierReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
