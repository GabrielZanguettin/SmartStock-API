using Microsoft.AspNetCore.Mvc;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartStock.Controllers
{
    [Route("api")]
    [ApiController]
    public class StockMovementController : ControllerBase
    {
        private readonly StockMovementService _stockMovementService;

        public StockMovementController(StockMovementService stockMovementService)
        {
            _stockMovementService = stockMovementService;
        }

        [HttpGet("stock-movements")]
        [SwaggerOperation(
            Summary = "List stock movements",
            Description = "Returns a filtered (and optionally paginated) list of stock movements based on the provided query parameters."
        )]
        public Task<IActionResult> FindAll([FromQuery] StockMovementFiltersDto? filters)
        {
            throw new NotImplementedException();
        }

        [HttpGet("stock-movements/{id:int}")]
        [SwaggerOperation(
            Summary = "Get stock movement by ID",
            Description = "Fetches a single stock movement that matches the specified identifier."
        )]
        public Task<IActionResult> FindOne([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("stock-movements")]
        [SwaggerOperation(
            Summary = "Create a new stock movement",
            Description = "Creates a new stock movement with the provided data and returns the created resource."
        )]
        public Task<IActionResult> Create([FromBody] StockMovement movement)
        {
            throw new NotImplementedException();
        }

        [HttpPut("stock-movements/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing stock movement",
            Description = "Updates an existing stock movement with the provided data based on the specified identifier."
        )]
        public Task<IActionResult> Update([FromRoute] int id, [FromBody] StockMovement movement)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("stock-movements/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a stock movement",
            Description = "Deletes the stock movement that matches the specified identifier."
        )]
        public Task<IActionResult> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}
