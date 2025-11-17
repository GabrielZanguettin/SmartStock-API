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
        public Task<IActionResult> FindAll([FromQuery] OrderItemFiltersDto? filters)
        {
            throw new NotImplementedException();
        }

        [HttpGet("order-items/{id:int}")]
        [SwaggerOperation(
            Summary = "Get order item by ID",
            Description = "Fetches a single order item that matches the specified identifier."
        )]
        public Task<IActionResult> FindOne([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("order-items")]
        [SwaggerOperation(
            Summary = "Create a new order item",
            Description = "Creates a new order item with the provided data and returns the created resource."
        )]
        public Task<IActionResult> Create([FromBody] OrderItem item)
        {
            throw new NotImplementedException();
        }

        [HttpPut("order-items/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing order item",
            Description = "Updates an existing order item with the provided data based on the specified identifier."
        )]
        public Task<IActionResult> Update([FromRoute] int id, [FromBody] OrderItem item)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("order-items/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete an order item",
            Description = "Deletes the order item that matches the specified identifier."
        )]
        public Task<IActionResult> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}
