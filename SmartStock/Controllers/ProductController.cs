using Microsoft.AspNetCore.Mvc;
using SmartStock.Dtos;
using SmartStock.Entities;
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
        public Task<IActionResult> FindAll([FromQuery] ProductFiltersDto? filters)
        {
            throw new NotImplementedException();
        }

        [HttpGet("products/{id:int}")]
        [SwaggerOperation(
            Summary = "Get product by ID",
            Description = "Fetches a single product that matches the specified identifier."
        )]
        public Task<IActionResult> FindOne([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("products")]
        [SwaggerOperation(
            Summary = "Create a new product",
            Description = "Creates a new product with the provided data and returns the created resource."
        )]
        public Task<IActionResult> Create([FromBody] Product product)
        {
            throw new NotImplementedException();
        }

        [HttpPut("products/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing product",
            Description = "Updates an existing product with the provided data based on the specified identifier."
        )]
        public Task<IActionResult> Update([FromRoute] int id, [FromBody] Product product)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("products/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a product",
            Description = "Deletes the product that matches the specified identifier."
        )]
        public Task<IActionResult> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}
