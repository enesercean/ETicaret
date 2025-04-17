using MediatR;
using Microsoft.AspNetCore.Identity;
using ETicaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using System.Threading.Tasks;
using System.Threading;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Abstractions.Services;

namespace ETicaretAPI.Application.Features.Commands.User.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {

            var token = await _authService.GoogleLoginAsync(request.IdToken, 1500);
            return new GoogleLoginCommandResponse { Token = token };


        }
    }
}




