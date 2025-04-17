using ETicaretAPI.Application.Repositories.BasketItem;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.BasketItem
{
    public class BasketItemReadRepository : ReadRepository<E.BasketItem>, IBasketItemReadRepository
    {
        public BasketItemReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
