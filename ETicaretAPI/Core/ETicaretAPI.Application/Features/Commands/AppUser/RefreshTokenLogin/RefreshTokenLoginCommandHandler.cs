using MediatR;
using Microsoft.AspNetCore.Identity;
using ETicaretAPI.Domain.Entities.Identity;
using ETicaretAPI.Application.Abstractions.Token;
using System.Threading.Tasks;
using System.Threading;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs;

namespace ETicaretAPI.Application.Features.Commands.User.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new()
            {
                Token = token
            };
        }
    }
}




