using MediatR;
using Microsoft.AspNetCore.Identity;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.Identity.Web;
using ETicaretAPI.Application.Exeptions;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.User;
using ETicaretAPI.Infrastructure.Constants;

namespace ETicaretAPI.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
        readonly IRoleService _roleService;

        public CreateUserCommandHandler(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {

            var user = new ETicaretAPI.Application.DTOs.User.CreateUser()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = request.NameSurname,
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password
            };

            CreateUserResponse result = await _userService.CreateAsync(user);

            await _roleService.AssignRoleAsync(user.Id, Roles.Customer);

            return new()
            {
                Success = result.Success,
                Message = result.Message
            };

        }
    }
}
