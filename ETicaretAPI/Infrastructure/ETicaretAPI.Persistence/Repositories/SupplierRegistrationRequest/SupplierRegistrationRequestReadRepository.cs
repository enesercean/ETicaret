using ETicaretAPI.Application.Repositories.ISupplierRegistrationRequest;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.SupplierRegistrationRequest
{
    public class SupplierRegistrationRequestReadRepository : ReadRepository<Domain.Entities.SupplierRegistrationRequest>, ISupplierRegistrationRequestReadRepository
    {
        public SupplierRegistrationRequestReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
