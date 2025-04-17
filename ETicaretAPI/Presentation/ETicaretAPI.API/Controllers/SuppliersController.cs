using ETicaretAPI.Application.Features.Queries.Supplier.GetSupplierByUser;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SuppliersController
    {
        readonly IMediator _mediator;
        public SuppliersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Get()
        {
            GetSupplierByUserQueryResponse response = await _mediator.Send(new GetSupplierByUserQueryRequest());
            return new OkObjectResult(response);
        }
    }
}
