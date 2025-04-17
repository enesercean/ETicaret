using ETicaretAPI.Application.Features.Commands.ProductRating.AddRating;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ETicaretAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductRatingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductRatingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> AddRating([FromBody] AddProductRatingCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response); 
        }
    }
}
