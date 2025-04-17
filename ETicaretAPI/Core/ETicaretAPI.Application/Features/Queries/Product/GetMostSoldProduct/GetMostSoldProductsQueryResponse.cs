using ETicaretAPI.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetMostSoldProduct
{
    public class GetMostSoldProductsQueryResponse
    {
        public List<MostSoldProduct> Products { get; set; }

    }
}
