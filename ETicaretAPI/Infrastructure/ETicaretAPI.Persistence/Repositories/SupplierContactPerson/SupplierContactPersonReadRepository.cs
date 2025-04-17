using ETicaretAPI.Application.Repositories.SupplierContactPerson;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.SupplierContactPerson
{
    public class SupplierContactPersonReadRepository : ReadRepository<Domain.Entities.SupplierContactPerson>, ISupplierContactPeopleReadRepository
    {
        public SupplierContactPersonReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
