using ETicaretAPI.Application.Repositories.Payment;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.Payment
{
    public class PaymentReadRepository : ReadRepository<E.Payment>, IPaymentReadRepository
    {
        public PaymentReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
