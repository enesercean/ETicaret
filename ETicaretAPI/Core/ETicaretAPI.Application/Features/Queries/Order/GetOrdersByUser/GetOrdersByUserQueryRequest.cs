using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Order.GetOrdersByUser
{
    public class GetOrdersByUserQueryRequest : IRequest<List<GetOrdersByUserQueryResponse>>
    {
    }
}
