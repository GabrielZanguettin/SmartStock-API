using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartStock.Dtos;
using SmartStock.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartStock.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("payments")]
        [SwaggerOperation(
            Summary = "List payments",
            Description = "Returns a filtered (and optionally paginated) list of payments based on the provided query parameters."
        )]
        public async Task<IActionResult> FindAll([FromQuery] PaymentFiltersDto? filters)
        {
            var response = await _paymentService.FindAll(filters);
            return Ok(response);
        }

        [HttpGet("payments/{id:int}")]
        [SwaggerOperation(
            Summary = "Get payment by ID",
            Description = "Fetches a single payment that matches the specified identifier."
        )]
        public async Task<IActionResult> FindOne([FromRoute] int id)
        {
            try
            {
                var response = await _paymentService.FindOne(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("payments")]
        [SwaggerOperation(
            Summary = "Create a new payment",
            Description = "Creates a new payment with the provided data and returns the created resource."
        )]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto)
        {
            try
            {
                var response = await _paymentService.CreatePayment(dto);
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
    }
}
