using Microsoft.AspNetCore.Mvc;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartStock.Controllers
{
    [Route("api")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("orders")]
        [SwaggerOperation(
            Summary = "List orders",
            Description = "Returns a filtered (and optionally paginated) list of orders based on the provided query parameters."
        )]
        public async Task<IActionResult> FindAll([FromQuery] OrderFiltersDto? filters)
        {
            var response = await _orderService.FindAll(filters);
            return Ok(response);
        }

        [HttpGet("orders/{id:int}")]
        [SwaggerOperation(
            Summary = "Get order by ID",
            Description = "Fetches a single order that matches the specified identifier."
        )]
        public async Task<IActionResult> FindOne([FromRoute] int id)
        {
            try
            {
                var response = await _orderService.FindOne(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("orders")]
        [SwaggerOperation(
            Summary = "Create a new order",
            Description = "Creates a new order with the provided data and returns the created resource."
        )]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            try
            {
                var response = await _orderService.CreateOrder(dto);
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

        [HttpPut("orders/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing order",
            Description = "Updates an existing order with the provided data based on the specified identifier."
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrderDto dto)
        {
            try
            {
                var response = await _orderService.UpdateOrder(id, dto);
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

        [HttpDelete("orders/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete an order",
            Description = "Deletes the order that matches the specified identifier."
        )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var response = await _orderService.DeleteOrder(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
