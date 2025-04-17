using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.SupplierOrder.GetDecompletedSupplierOrder
{
    public class GetDecompletedSupplierOrderQueryRequest : IRequest<List<GetDecompletedSupplierOrderQueryResponse>>
    {
        public Guid UserId { get; set; }
    }
}
