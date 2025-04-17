using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Product.GetRelatedRroductsByProductId
{
    public class GetRelatedRroductsByProductIdQueryRequest : IRequest<GetRelatedRroductsByProductIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
}
