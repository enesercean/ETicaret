using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Brand.GetBrandByCategory
{
    public class GetBrandByCategoryQueryResponse
    {
        public Guid BrandId { get; set; }
        public string Name { get; set; }
    }
}
