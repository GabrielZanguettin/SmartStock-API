using Microsoft.AspNetCore.Mvc;
using SmartStock.Dtos;
using SmartStock.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartStock.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("products")]
        [SwaggerOperation(
            Summary = "List products",
            Description = "Returns a filtered (and optionally paginated) list of products based on the provided query parameters."
        )]
        public async Task<IActionResult> FindAll([FromQuery] ProductFiltersDto? filters)
        {
            var response = await _productService.FindAll(filters);
            return Ok(response);
        }

        [HttpGet("products/{id:int}")]
        [SwaggerOperation(
            Summary = "Get product by ID",
            Description = "Fetches a single product that matches the specified identifier."
        )]
        public async Task<IActionResult> FindOne([FromRoute] int id)
        {
            try
            {
                var response = await _productService.FindOne(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("products")]
        [SwaggerOperation(
            Summary = "Create a new product",
            Description = "Creates a new product with the provided data and returns the created resource."
        )]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            try
            {
                var response = await _productService.CreateProduct(dto);
                return CreatedAtAction(nameof(FindOne), new { id = response.Data.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("products/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing product",
            Description = "Updates an existing product with the provided data based on the specified identifier."
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto dto)
        {
            try
            {
                var response = await _productService.UpdateProduct(id, dto);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("products/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a product",
            Description = "Deletes the product that matches the specified identifier."
        )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var response = await _productService.DeleteProduct(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
