using ETicaretAPI.Application.Features.Commands.Address.CreateAddress;
using ETicaretAPI.Application.Features.Commands.Address.RemoveAddress;
using ETicaretAPI.Application.Features.Commands.Address.UpdateAddress;
using ETicaretAPI.Application.Features.Queries.Address.GetAddressByUser;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : Controller
    {
        readonly IMediator _mediator;
        public AddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetAddressesByUser()
        {
            List<GetAddressByUserQueryResponse> response = await _mediator.Send(new GetAddressByUserQueryRequest());
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post([FromBody]CreateAddressCommandRequest createAddressCommandRequest)
        {
            CreateAddressCommandResponse createAddressCommandResponse = await _mediator.Send(createAddressCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new RemoveAddressCommandRequest { Id = id });
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Update([FromBody] UpdateAddressCommandRequest updateAddressCommandRequest)
        {
            UpdateAddressCommandResponse response = await _mediator.Send(updateAddressCommandRequest);
            return Ok(response);
        }
    }
}
