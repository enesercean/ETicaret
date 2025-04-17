using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogout
{
    public class GoogleLogOutCommandHandler : IRequestHandler<GoogleLogOutCommandRequest, GoogleLogOutCommandResponse>
    {
        readonly IAuthService _authService;
        readonly IUserService _userService;

        public GoogleLogOutCommandHandler(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async Task<GoogleLogOutCommandResponse> Handle(GoogleLogOutCommandRequest request, CancellationToken cancellationToken)
        {
            var userId = await _userService.GetCurrentUser();

            await _authService.GoogleLogoutAsync(userId.ToString());

            return new GoogleLogOutCommandResponse { Success = true, Message = "Google logout successful" };
        }
    }
}
