using ETicaretAPI.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetRelatedRroductsByProductId
{
    public class GetRelatedRroductsByProductIdQueryResponse
    {
        public List<ListProduct> listProducts {  get; set; }
    }
}
