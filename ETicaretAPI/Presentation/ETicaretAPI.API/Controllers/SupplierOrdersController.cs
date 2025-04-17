using ETicaretAPI.Application.Features.Commands.CompletedOrder.CreateCompletedOrder;
using ETicaretAPI.Application.Features.Queries.SupplierOrder.GetDecompletedSupplierOrder;
using ETicaretAPI.Application.Features.Queries.SupplierOrder.GetIncompletedSupplierOrder;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = DefaultPolicy.EmployeeReadPolicy, AuthenticationSchemes = "Admin")]
    public class SupplierOrdersController : Controller
    {
        readonly IMediator _mediator;
        readonly IHttpContextAccessor _httpContextAccessor;
        public SupplierOrdersController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Authorize(Policy = DefaultPolicy.EmployeeReadPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetSupplierOrders()
        {
            GetDecompletedSupplierOrderQueryRequest getSupplierOrderQueryRequest = new GetDecompletedSupplierOrderQueryRequest();
            getSupplierOrderQueryRequest.UserId = Guid.Parse(UserId);
            List<GetDecompletedSupplierOrderQueryResponse> response = await _mediator.Send(getSupplierOrderQueryRequest);
            return Ok(response);
        }

        [HttpGet("incomplete")]
        [Authorize(Policy = DefaultPolicy.EmployeeReadPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetIncompleteSupplierOrders()
        {
            GetIncompleteSupplierOrderQueryRequest getIncompleteSupplierOrderQueryRequest = new GetIncompleteSupplierOrderQueryRequest();
            getIncompleteSupplierOrderQueryRequest.UserId = Guid.Parse(UserId);
            List<GetIncompleteSupplierOrderQueryResponse> response = await _mediator.Send(getIncompleteSupplierOrderQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Policy = DefaultPolicy.EmployeePolicy)]
        public async Task<IActionResult> CreateCompletedOrder([FromBody] CreateCompletedOrderCommandRequest completedOrderCommandRequest)
        {
            CreateCompletedOrderCommandResponse response = await _mediator.Send(completedOrderCommandRequest);
            return Ok();
        }

        protected string UserId
        {
            get
            {
                var userId = _httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("User not found");
                }

                return userId;
            }
        }
    }
}

