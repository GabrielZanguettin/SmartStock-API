using Microsoft.AspNetCore.Mvc;
using SmartStock.Dtos;
using SmartStock.Entities;
using SmartStock.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace SmartStock.Controllers
{
    [Route("api")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly SupplierService _supplierService;

        public SupplierController(SupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet("suppliers")]
        [SwaggerOperation(
            Summary = "List suppliers",
            Description = "Returns a filtered (and optionally paginated) list of suppliers based on the provided query parameters."
        )]
        public async Task<IActionResult> FindAll([FromQuery] SupplierFiltersDto? filters)
        {
            var response = await _supplierService.FindAll(filters);
            return Ok(response);
        }

        [HttpGet("suppliers/{id:int}")]
        [SwaggerOperation(
            Summary = "Get supplier by ID",
            Description = "Fetches a single supplier that matches the specified identifier."
        )]
        public async Task<IActionResult> FindOne([FromRoute] int id)
        {
            try
            {
                var response = await _supplierService.FindOne(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost("suppliers")]
        [SwaggerOperation(
            Summary = "Create a new supplier",
            Description = "Creates a new supplier with the provided data and returns the created resource."
        )]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDto dto)
        {
            try
            {
                var response = await _supplierService.CreateSupplier(dto);
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

        [HttpPatch("suppliers/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing supplier",
            Description = "Updates an existing supplier with the provided data based on the specified identifier."
        )]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSupplierDto dto)
        {
            try
            {
                var response = await _supplierService.UpdateSupplier(id, dto);
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

        [HttpDelete("suppliers/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a supplier",
            Description = "Deletes the supplier that matches the specified identifier."
        )]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var response = await _supplierService.DeleteSupplier(id);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
