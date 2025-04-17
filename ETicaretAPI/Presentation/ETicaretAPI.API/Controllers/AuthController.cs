using ETicaretAPI.Application.Features.Commands.AppUser.GoogleLogout;
using ETicaretAPI.Application.Features.Commands.User.GoogleLogin;
using ETicaretAPI.Application.Features.Commands.User.LoginUser;
using ETicaretAPI.Application.Features.Commands.User.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
            LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Admin", Policy = "CustomerPolicy")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokenLogin([FromBody] RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
        {
            RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginCommandRequest googleLoginCommandRequest)
        {
            GoogleLoginCommandResponse response = await _mediator.Send(googleLoginCommandRequest);
            return Ok(response);
        }

        [HttpPost("google-logout")]
        public async Task<IActionResult> GoogleLogout([FromBody] GoogleLogOutCommandRequest googleLogOutCommandRequest)
        {
            GoogleLogOutCommandResponse response = await _mediator.Send(googleLogOutCommandRequest);
            return Ok(response);
        }
    }
}
