using Microsoft.AspNetCore.Mvc;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartStock.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _orderItemService;

        public OrderItemController(OrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("order-items")]
        [SwaggerOperation(
            Summary = "List order items",
            Description = "Returns a filtered (and optionally paginated) list of order items based on the provided query parameters."
        )]
        public async Task<IActionResult> FindAll([FromQuery] OrderItemFiltersDto? filters)
        {
            var response = await _orderItemService.FindAll(filters);
            return Ok(response);
        }

        [HttpGet("order-items/{id:int}")]
        [SwaggerOperation(
            Summary = "Get order item by ID",
            Description = "Fetches a single order item that matches the specified identifier."
        )]
        public async Task<IActionResult> FindOne([FromRoute] int id)
        {
            try
            {
                var response = await _orderItemService.FindOne(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("order-items")]
        [SwaggerOperation(
            Summary = "Create a new order item",
            Description = "Creates a new order item with the provided data and returns the created resource."
        )]
        public async Task<IActionResult> Create([FromBody] CreateOrderItemDto dto)
        {
            try
            {
                var response = await _orderItemService.CreateOrderItem(dto);
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

        [HttpPatch("order-items/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing order item",
            Description = "Updates an existing order item with the provided data based on the specified identifier."
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderItemDto dto)
        {
            try
            {
                var response = await _orderItemService.UpdateOrderItem(id, dto);
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

        [HttpDelete("order-items/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete an order item",
            Description = "Deletes the order item that matches the specified identifier."
        )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var response = await _orderItemService.DeleteOrderItem(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
