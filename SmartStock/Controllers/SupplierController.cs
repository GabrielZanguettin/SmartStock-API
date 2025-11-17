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
        public Task<IActionResult> FindAll([FromQuery] SupplierFiltersDto? filters)
        {
            throw new NotImplementedException();
        }

        [HttpGet("suppliers/{id:int}")]
        [SwaggerOperation(
            Summary = "Get supplier by ID",
            Description = "Fetches a single supplier that matches the specified identifier."
        )]
        public Task<IActionResult> FindOne([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("suppliers")]
        [SwaggerOperation(
            Summary = "Create a new supplier",
            Description = "Creates a new supplier with the provided data and returns the created resource."
        )]
        public Task<IActionResult> Create([FromBody] Supplier supplier)
        {
            throw new NotImplementedException();
        }

        [HttpPut("suppliers/{id:int}")]
        [SwaggerOperation(
            Summary = "Update an existing supplier",
            Description = "Updates an existing supplier with the provided data based on the specified identifier."
        )]
        public Task<IActionResult> Update([FromRoute] int id, [FromBody] Supplier supplier)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("suppliers/{id:int}")]
        [SwaggerOperation(
            Summary = "Delete a supplier",
            Description = "Deletes the supplier that matches the specified identifier."
        )]
        public Task<IActionResult> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}
