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
        public Task<IActionResult> FindAll([FromQuery] OrderFiltersDto? filters)
        {
            throw new NotImplementedException();
        }

        [HttpGet("orders/{id:int}")]
        [SwaggerOperation(
            Summary = "Get order by ID",
            Description = "Fetches a single order that matches the specified identifier."
        )]
        public Task<IActionResult> FindOne([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("orders")]
        [SwaggerOperation(
            Summary = "Create a new order",
            Description = "Creates a new order with the provided data and returns the created resource."
        )]
        public Task<IActionResult> Create([FromBody] Order order)
        {
            throw new NotImplementedException();
        }

        [HttpPut("orders/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing order",
            Description = "Updates an existing order with the provided data based on the specified identifier."
        )]
        public Task<IActionResult> Update([FromRoute] int id, [FromBody] Order order)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("orders/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete an order",
            Description = "Deletes the order that matches the specified identifier."
        )]
        public Task<IActionResult> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}
