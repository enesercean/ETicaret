using ETicaretAPI.Application.Repositories.ProductRating;
using ETicaretAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories.ProductRating
{
    public class ProductRatingWriteRepository : WriteRepository<Domain.Entities.ProductRating>, IProductRatingWriteRepository
    {
        public ProductRatingWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
