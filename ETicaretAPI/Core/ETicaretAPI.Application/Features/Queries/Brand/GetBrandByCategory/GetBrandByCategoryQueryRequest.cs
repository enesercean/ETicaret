using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Brand.GetBrandByCategory
{
    public class GetBrandByCategoryQueryRequest : IRequest<List<GetBrandByCategoryQueryResponse>>
    {
        public List<string> CategoryIdList { get; set; }
    }
}
