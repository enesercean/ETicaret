using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Queries.User.GetUserProfileDetail
{
    public class GetUserProfileDetailQueryHandler : IRequestHandler<GetUserProfileDetailQueryRequest, GetUserProfileDetailQueryResponse>
    {
        readonly IUserService _userService;
        readonly IRoleService _roleService;
        public GetUserProfileDetailQueryHandler(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        public async Task<GetUserProfileDetailQueryResponse> Handle(GetUserProfileDetailQueryRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUserId();

            if (userId == null)
            {
                throw new Exception("User not found");
            }

            var user = await _userService.GetUserById(userId.ToString());
            var role = await _roleService.GetUserRolesAsync(userId.ToString());

            return new()
            {
                Id = user.Id.ToString(),
                Username = user.UserName,
                Name = user.NameSurname,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                RegistrationDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
                SubscriptionType = role.First()
            };
        }
    }
}
