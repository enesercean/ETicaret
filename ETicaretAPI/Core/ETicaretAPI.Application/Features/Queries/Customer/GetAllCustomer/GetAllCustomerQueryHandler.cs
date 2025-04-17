using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.Customer.GetAllCustomer
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQueryRequest, List<GetAllCustomerQueryResponse>>
    {
        public Task<List<GetAllCustomerQueryResponse>> Handle(GetAllCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
