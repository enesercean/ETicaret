using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Features.Commands.Brand.CreateBrand;
using ETicaretAPI.Application.Features.Queries.Brand.GetBrandByCategory;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ETicaretAPI.Application.Features.Queries.Brand.GetAllBrands;

namespace ETicaretAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class BrandsController : Controller
    {
        readonly IMediator _mediator;

        public BrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("getByCategories")]
        public async Task<IActionResult> GetByCategories([FromBody] GetBrandByCategoryQueryRequest getBrandByCategoryQueryRequest)
        {
            List<GetBrandByCategoryQueryResponse> response = await _mediator.Send(getBrandByCategoryQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.EmployeePolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> CreateBrand([FromBody] CreateBrandCommandRequest createBrandCommandRequest)
        {
            CreateBrandCommandResponse response = await _mediator.Send(createBrandCommandRequest);
            
            if (response.Success)
                return StatusCode(201, response);
                
            return BadRequest(response);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllBrands()
        {
            var response = await _mediator.Send(new GetAllBrandsQueryRequest());
            return Ok(response);
        }
    }
}
