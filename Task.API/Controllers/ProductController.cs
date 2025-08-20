using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Features.Auth.Commands.Register;
using Task.Application.Features.Products.Commands.CreateProduct;
using Task.Application.Features.Products.Commands.DeleteProduct;
using Task.Application.Features.Products.Commands.UpdateProduct;
using Task.Application.Features.Products.Queries;
using Task.Application.Features.Products.Queries.GetAllProduct;
using Task.Application.Features.Products.Queries.GetProductById;

namespace Task.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("createproduct")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productId = await _mediator.Send(request);

            return CreatedAtAction(nameof(CreateProduct), new { id = productId },
                          new { Message = "Ürün başarıyla kaydedildi.", ProductId = productId });
        }

        [HttpPut("updateproduct/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Id = id;

            var result = await _mediator.Send(request);

            return Ok(new
            {
                Message = $"Ürün başarıyla güncellendi. Güncellenen ürün Id: {result}",
                UpdatedProductId = result
            });
        }

        [HttpDelete("deleteproduct/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand { Id = id });

            if (!result)
                return NotFound(new { Message = "Ürün bulunamadı veya silinemedi." });

            return Ok(new { Message = "Ürün başarıyla silindi." });
        }

        [HttpGet("getallproduct")]
        public async Task<IActionResult> GetAllProduct()
        {
            var productDtos = await _mediator.Send(new GetAllProductsQuery());
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
          
            return Ok(product);
        }
    }

}

