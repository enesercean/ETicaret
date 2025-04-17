using ETicaretAPI.Application.Repositories.ProductRating;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.ProductRating
{
    public class ProductRatingReadRepository : ReadRepository<Domain.Entities.ProductRating>, IProductRatingReadRepository
    {
        public ProductRatingReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
