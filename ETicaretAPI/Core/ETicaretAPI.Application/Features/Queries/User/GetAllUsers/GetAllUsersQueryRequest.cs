using MediatR;
using System.Collections.Generic;

namespace ETicaretAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<List<GetAllUsersQueryResponse>>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 20;
    }
}
