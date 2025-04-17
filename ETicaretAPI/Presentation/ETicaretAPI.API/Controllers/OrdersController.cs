
using ETicaretAPI.Application.Features.Commands.Order.CreateOrder;
using ETicaretAPI.Application.Features.Queries.CompletedOrder.GetAllCompletedOrder;
using ETicaretAPI.Application.Features.Queries.Order.GetAllOrder;
using ETicaretAPI.Application.Features.Queries.Order.GetOrdersByUser;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin", Policy = DefaultPolicy.CustomerPolicy)]
    public class OrdersController : Controller
    {
        readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin", Policy = DefaultPolicy.EmployeeReadPolicy)]
        public async Task<IActionResult> GetAllOrders([FromQuery]GetAllOrderQueryRequest getAllOrderQueryRequest)
        {
            List<GetAllOrderQueryResponse> response = await _mediator.Send(getAllOrderQueryRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Policy = DefaultPolicy.EmployeeReadPolicy)]
        public async Task<IActionResult> GetAllCompletedOrders([FromQuery] GetAllCompletedOrderQueryRequest getAllCompletedOrderQueryRequest)
        {
            List<GetAllCompletedOrderQueryResponse> response = await _mediator.Send(getAllCompletedOrderQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin", Policy = DefaultPolicy.CustomerPolicy)]
        public async Task<IActionResult> CreateOrder([FromBody]CreateOrderCommandRequest createOrderCommandRequest)
        {
            CreateOrderCommandResponse response = await _mediator.Send(createOrderCommandRequest);
            return Ok(response);
        }
        [HttpGet("[action]")]
        [Authorize(AuthenticationSchemes = "Admin", Policy = DefaultPolicy.EmployeeReadPolicy)]
        public async Task<IActionResult> GetOrdersByUser()
        {
            List<GetOrdersByUserQueryResponse> response = await _mediator.Send(new GetOrdersByUserQueryRequest());
            return Ok(response);
        }
    }
}
