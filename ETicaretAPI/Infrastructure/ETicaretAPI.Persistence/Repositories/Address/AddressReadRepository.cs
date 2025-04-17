using ETicaretAPI.Application.Repositories.Address;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.Address
{
    public class AddressReadRepository : ReadRepository<Domain.Entities.Address>, IAddressReadRepository
    {
        public AddressReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
