using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetUserByUserName
{
    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQueryRequest, GetUserByUserNameQueryResponse>
    {
        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public GetUserByUserNameQueryHandler(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<GetUserByUserNameQueryResponse> Handle(GetUserByUserNameQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByUserName(request.UserName);


            return new()
            {
                Id = user.Id,
                FirstName = user.NameSurname,
                Username = user.UserName,
                Email = user.Email
            };
        }
    }
}
