using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetProductByCategoryAndBrand
{
    public class GetProductByCategoryAndBrandQueryRequest : IRequest<GetProductByCategoryAndBrandQueryResponse>
    {
        public Guid BrandId { get; set; }
        public List<Guid> CategoryIds { get; set; }
    }
}
