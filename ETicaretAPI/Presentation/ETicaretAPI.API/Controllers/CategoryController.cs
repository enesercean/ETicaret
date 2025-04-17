using ETicaretAPI.Application.Features.Commands.Category.CreateCategory;
using ETicaretAPI.Application.Features.Queries.ProductCategory.GetAllProductCategory;
using ETicaretAPI.Application.Policy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory([FromQuery]GetAllProductCategoryRequest getAllProductCategoryRequest)
        {
            GetAllProductCategoryResponse response = await _mediator.Send(getAllProductCategoryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.EmployeePolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommandRequest createCategoryCommandRequest)
        {
            CreateCategoryCommandResponse response = await _mediator.Send(createCategoryCommandRequest);
            
            if (response.Success)
                return StatusCode(201, response);
            
            return BadRequest(response);
        }
    }
}
