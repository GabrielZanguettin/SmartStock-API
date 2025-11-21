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
        public async Task<IActionResult> FindAll([FromQuery] StockMovementFiltersDto? filters)
        {
            var response = await _stockMovementService.FindAll(filters);
            return Ok(response);
        }

        [HttpGet("stock-movements/{id:int}")]
        [SwaggerOperation(
            Summary = "Get stock movement by ID",
            Description = "Fetches a single stock movement that matches the specified identifier."
        )]
        public async Task<IActionResult> FindOne([FromRoute] int id)
        {
            try
            {
                var response = await _stockMovementService.FindOne(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("stock-movements")]
        [SwaggerOperation(
            Summary = "Create a new stock movement",
            Description = "Creates a new stock movement with the provided data and returns the created resource."
        )]
        public async Task<IActionResult> Create([FromBody] CreateStockMovementDto dto)
        {
            try
            {
                var response = await _stockMovementService.CreateStockMovement(dto);
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

        [HttpPatch("stock-movements/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing stock movement",
            Description = "Updates an existing stock movement with the provided data based on the specified identifier."
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockMovementDto dto)
        {
            try
            {
                var response = await _stockMovementService.UpdateStockMovement(id, dto);
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

        [HttpDelete("stock-movements/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a stock movement",
            Description = "Deletes the stock movement that matches the specified identifier."
        )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var response = await _stockMovementService.DeleteStockMovement(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
