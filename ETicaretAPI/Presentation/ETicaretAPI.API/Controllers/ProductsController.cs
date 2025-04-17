using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Linq;
using System.Data;
using MediatR;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetProductById;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.Product.DeleteProduct;
using ETicaretAPI.Application.Features.Commands.Product.UploadProductImage;
using ETicaretAPI.Application.Features.Queries.Product.GetProductImage;
using ETicaretAPI.Application.Features.Queries.Product.GetProductImageFileUrl;
using ETicaretAPI.Application.Features.Commands.Product.DeleteProductImageFile;
using Microsoft.AspNetCore.Authorization;
using ETicaretAPI.Application.Features.Queries.ProductCategory.GetAllProductCategory;
using ETicaretAPI.Application.Features.Queries.Product.GetProductWithCategory;
using ETicaretAPI.Application.Features.Queries.Product.GetProductByCategoryAndBrand;
using ETicaretAPI.Application.Policy;
using ETicaretAPI.Application.Features.Queries.ProductLike.GetMostLikedProducts;
using ETicaretAPI.Application.Features.Queries.Product.GetMostSoldProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetRelatedRroductsByProductId;
using ETicaretAPI.Application.Features.Queries.Product.GetProductBySupplier;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            GetAllProductQueryResponse response = await _mediator.Send(new GetAllProductQueryRequest());
            return Ok(response);
        }
        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] GetProductByIdQueryRequest getProductByIdQueryRequest)
        {
            GetProductByIdQueryResponse response = await _mediator.Send(getProductByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = DefaultPolicy.EmployeePolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {
            CreateProductCommandResponse createProductCommandResponse = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(Policy = DefaultPolicy.EmployeePolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put(UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = DefaultPolicy.EmployeePolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest deleteProductCommandRequest)
        {
            DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
            if (!response.Success)
                return BadRequest(response.Message);
            return Ok();
        }
        [HttpPost("[action]")]
        [Authorize(Policy = DefaultPolicy.EmployeePolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> upload(string id, UploadProductImageCommandRequest? uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            uploadProductImageCommandRequest.Id = id;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }
        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductImage([FromRoute] GetProductImageQueryRequest getProductImageQueryRequest)
        {
            GetProductImageQueryResponse response = await _mediator.Send(getProductImageQueryRequest);
            return Ok(response.ProductImages);
        }

        [HttpGet("[action]/{Id}/{FileName}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductImage([FromRoute] GetProductImageFileQueryRequest getProductImageFileQueryRequest)
        {
            GetProductImageFileQueryResponse response = await _mediator.Send(getProductImageFileQueryRequest);
            if (response == null)
            {
                return NotFound("Resim bulunamadı.");
            }
            return File(response.ImageBytes, response.ContentType);
        }

        [HttpDelete("[action]/{ProductId}/{ImageId}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute]DeleteProductImageFileCommandRequest deleteProductImageFileRequest)
        {
            DeleteProductImageFileCommandResponse response = await _mediator.Send(deleteProductImageFileRequest);
            return Ok();
        }

        
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductByCategory([FromBody]GetProductWithCategoryRequest getProductWithCategoryRequest)
        {
            GetProductWithCategoryResponse response = await _mediator.Send(getProductWithCategoryRequest);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetProductByCategoryAndBrand([FromBody] GetProductByCategoryAndBrandQueryRequest getProductByCategoryAndBrandQueryRequest)
        {
            GetProductByCategoryAndBrandQueryResponse response = await _mediator.Send(getProductByCategoryAndBrandQueryRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMostSoldProducts()
        {
            GetMostSoldProductsQueryRequest request = new GetMostSoldProductsQueryRequest();
            request.Count = 10;
            GetMostSoldProductsQueryResponse response = await _mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}/related")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRelatedProducts([FromRoute] GetRelatedRroductsByProductIdQueryRequest getRelatedRroductsByProductIdQueryRequest)
        {
            GetRelatedRroductsByProductIdQueryResponse response = await _mediator.Send(getRelatedRroductsByProductIdQueryRequest);
            return Ok(response);
        }

        [HttpGet("[action]")]
        [Authorize(Policy = DefaultPolicy.EmployeePolicy, AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetProductBySupplier()
        {
            GetProductBySupplierQueryResponse response = await _mediator.Send(new GetProductBySupplierQueryRequest());
            return Ok(response);
        }
    }
}
