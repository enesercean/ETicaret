using ETicaretAPI.Application.Features.Commands.ProductLike;
using ETicaretAPI.Application.Features.Queries.ProductLike.GetMostLikedProducts;
using ETicaretAPI.Application.Features.Queries.ProductLike.GetUserLikedProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ETicaretAPI.Application.Features.Commands.ProductLike.AddProductLike;
using ETicaretAPI.Application.Features.Queries.ProductLike.CheckUserLiked;
using ETicaretAPI.Application.Policy;
using ETicaretAPI.Application.Features.Commands.ProductLike.DeleteProduckLike;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductLikeController : ControllerBase
    {
        readonly IMediator _mediator;

        public ProductLikeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.EmployeeReadPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> AddLike([FromBody] AddProductLikeCommandRequest addProductLikeCommandRequest)
        {
            AddProductLikeCommandResponse response = await _mediator.Send(addProductLikeCommandRequest);
            return Ok(response);
        }

        [HttpGet("user/liked")]
        [Authorize(Policy = DefaultPolicy.EmployeeReadPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetUserLikedProducts()
        {
            GetUserLikedProductsQueryRequest request = new GetUserLikedProductsQueryRequest();
            GetUserLikedProductsQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("most-liked")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMostLikedProducts([FromQuery] GetMostLikedProductsQueryRequest getMostLikedProductsQueryRequest)
        {
            GetMostLikedProductsQueryResponse response = await _mediator.Send(getMostLikedProductsQueryRequest);
            return Ok(response);
        }

        [HttpGet("user/liked/{productId}")]
        [Authorize]
        public async Task<IActionResult> CheckUserLikedProduct(Guid productId)
        {
            CheckUserLikedQueryRequest request = new CheckUserLikedQueryRequest { ProductId = productId };
            CheckUserLikedQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = DefaultPolicy.CustomerPolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> RemoveLike(Guid id)
        {
            DeleteProductLikeCommandRequest removeProductLikeCommandRequest = new DeleteProductLikeCommandRequest { ProductId = id };
            DeleteProductLikeCommandResponse response = await _mediator.Send(removeProductLikeCommandRequest);
            return Ok(response);
        }

    }
}
