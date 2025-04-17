using ETicaretAPI.Application.Repositories.ProductLike;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E = ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Repositories.ProductLike
{
    public class ProductLikeWriteRepository : WriteRepository<E.ProductLike>, IProductLikeWriteRepository
    {
        public ProductLikeWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
