using ETicaretAPI.Application.Features.Commands.SupplierRegistrationRequest.AddSuplplierRegistrationRequest;
using ETicaretAPI.Application.Features.Commands.SupplierRegistrationRequest.ApproveSupplierRegistrationRequest;
using ETicaretAPI.Application.Features.Queries.SupplierRegistrationRequest.GetSupplierRegistrationRequests;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierRegistrationController : Controller
    {
        readonly IMediator _mediator;

        public SupplierRegistrationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> AddSupplierRegistrationRequest([FromBody] AddSupplierRegistrationRequestCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = DefaultPolicy.AdminPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetSupplierRegistrationRequests()
        {
            var response = await _mediator.Send(new GetSupplierRegistrationRequestsQueryRequest());
            return Ok(response);
        }

        [HttpPut("approve")]
        [Authorize(Policy = DefaultPolicy.AdminPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> ApproveSupplierRegistrationRequest([FromBody] ApproveSupplierRegistrationRequestCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
