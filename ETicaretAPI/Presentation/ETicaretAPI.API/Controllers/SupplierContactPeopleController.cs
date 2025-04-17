using ETicaretAPI.Application.Features.Commands.SupplierContactPeople.CreateSupplierContactPeople;
using ETicaretAPI.Application.Features.Commands.SupplierContactPeople.DeleteSupplierContactPeople;
using ETicaretAPI.Application.Features.Commands.SupplierContactPeople.GetSupplierContactPeopleByUser;
using ETicaretAPI.Application.Features.Commands.SupplierContactPeople.UpdateUserRole;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierContactPeopleController : Controller
    {
        readonly IMediator _mediator;

        public SupplierContactPeopleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = DefaultPolicy.SupplierManagerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetUserDetail()
        {
            List<GetSupplierContactPeopleByUserQueryResponse> response = await _mediator.Send(new GetSupplierContactPeopleByUserQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.SupplierManagerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> CreateSupplierContactPerson([FromBody]CreateSupplierContactPeopleCommandRequest createSupplierContactPersonCommandRequest)
        {
            CreateSupplierContactPeopleCommandResponse response = await _mediator.Send(createSupplierContactPersonCommandRequest);
            return Ok(response);
        }

        [HttpPost("update-user-role")]
        [Authorize(Policy = DefaultPolicy.SupplierManagerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> UpdateUserRole([FromBody]UpdateUserRoleCommandRequest updateUserRoleCommandRequest)
        {
            UpdateUserRoleCommandResponse response = await _mediator.Send(updateUserRoleCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = DefaultPolicy.SupplierManagerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteSupplierContactPerson([FromRoute] DeleteSupplierContactPeopleCommandRequest deleteSupplierContactPeopleCommandRequest)
        {
            DeleteSupplierContactPeopleCommandResponse response = await _mediator.Send(deleteSupplierContactPeopleCommandRequest);
            return Ok(response);
        }
    }
}
