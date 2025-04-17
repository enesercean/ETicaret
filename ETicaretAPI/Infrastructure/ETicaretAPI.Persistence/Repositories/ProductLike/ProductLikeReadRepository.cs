using ETicaretAPI.Application.Repositories.ProductLike;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.ProductLike
{
    public class ProductLikeReadRepository : ReadRepository<Domain.Entities.ProductLike>, IProductLikeReadRepository
    {
        public ProductLikeReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
