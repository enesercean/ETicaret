using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, List<GetAllUsersQueryResponse>>
    {
        private readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<List<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync();
            
            var paginatedUsers = users
                .Skip(request.Page * request.Size)
                .Take(request.Size)
                .ToList();

            return paginatedUsers.Select(user => new GetAllUsersQueryResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                PhoneNumber = user.PhoneNumber
            }).ToList();
        }
    }
}
