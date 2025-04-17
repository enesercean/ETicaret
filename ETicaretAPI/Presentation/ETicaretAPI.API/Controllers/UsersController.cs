using ETicaretAPI.Application.Features.Commands.AppUser.UpdateProfile;
using ETicaretAPI.Application.Features.Commands.User.CreateUser;
using ETicaretAPI.Application.Features.Commands.User.GoogleLogin;
using ETicaretAPI.Application.Features.Commands.User.LoginUser;
using ETicaretAPI.Application.Features.Commands.User.RefreshTokenLogin;
using ETicaretAPI.Application.Features.Queries.User.GetAllUsers;
using ETicaretAPI.Application.Features.Queries.User.GetUserByUserName;
using ETicaretAPI.Application.Features.Queries.User.GetUserProfileDetail;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpGet("[action]/{username}")]
        [Authorize(Policy = DefaultPolicy.SupplierManagerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetUserByUserName([FromRoute]GetUserByUserNameQueryRequest getUserByIdQueryRequest)
        {
            GetUserByUserNameQueryResponse response = await _mediator.Send(getUserByIdQueryRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetProfile()
        {
            GetUserProfileDetailQueryResponse response = await _mediator.Send(new GetUserProfileDetailQueryRequest());
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCommandRequest updateProfileCommandRequest)
        {
            UpdateProfileCommandResponse response = await _mediator.Send(updateProfileCommandRequest);
            return Ok(response);
        }

        [HttpGet("all")]
        [Authorize(Policy = DefaultPolicy.AdminPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            List<GetAllUsersQueryResponse> response = await _mediator.Send(new GetAllUsersQueryRequest());
            return Ok(response);
        }
    }
}
