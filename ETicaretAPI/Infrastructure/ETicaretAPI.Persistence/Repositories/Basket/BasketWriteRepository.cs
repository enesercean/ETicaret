using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.Repositories.Basket;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.Basket
{
    public class BasketWriteRepository : WriteRepository<E.Basket>, IBasketWriteRepository
    {
        public BasketWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
