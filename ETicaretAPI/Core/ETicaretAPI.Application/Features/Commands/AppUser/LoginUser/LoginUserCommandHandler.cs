using MediatR;
using Microsoft.AspNetCore.Identity;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Abstractions.Services;

namespace ETicaretAPI.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.LoginAsync(request.UserNameOrEmail, request.Password, 1500);
            return new LoginUserSuccessResponse { Token = token };

        }
    }
}
