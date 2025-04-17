using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetUserByUserName
{
    public class GetUserByUserNameQueryRequest : IRequest<GetUserByUserNameQueryResponse>
    {
        public string UserName { get; set; }
    }
}
