using ETicaretAPI.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Features.Commands.AppUser.UpdateProfile
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommandRequest, UpdateProfileCommandResponse>
    {
        readonly IUserService _userService;

        public UpdateProfileCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdateProfileCommandResponse> Handle(UpdateProfileCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserProfile(request.Name, request.PhoneNumber);


            return new()
            {
                success = true,
                message = ""
            };
        }
    }
}
