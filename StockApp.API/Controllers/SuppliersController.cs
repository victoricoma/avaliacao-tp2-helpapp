using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet(Name = "GetSuppliers")]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> Get()
        {
            var suppliers = await _supplierService.GetSuppliers();
            if (suppliers == null)
            {
                return NotFound("Suppliers not found");
            }
            return Ok(suppliers);
        }

        [HttpGet("{id:int}", Name = "GetSupplier")]
        public async Task<ActionResult<SupplierDTO>> Get(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }
            return Ok(supplier);
        }

        [HttpPost(Name = "CreateSupplier")]
        public async Task<ActionResult> Post([FromBody] SupplierDTO supplierDTO)
        {
            if (supplierDTO == null)
            {
                return BadRequest("Invalid data");
            }

            await _supplierService.Add(supplierDTO);

            return new CreatedAtRouteResult("GetSupplier",
                new { id = supplierDTO.Id }, supplierDTO);
        }

        [HttpPut(Name = "UpdateSupplier")]
        public async Task<ActionResult> Put(int id, [FromBody] SupplierDTO supplierDTO)
        {
            if (id != supplierDTO.Id)
            {
                return BadRequest("Inconsistent Id");
            }

            if (supplierDTO == null)
            {
                return BadRequest("Invalid update data");
            }

            await _supplierService.Update(supplierDTO);

            return Ok(supplierDTO);
        }

        [HttpDelete("{id:int}", Name = "DeleteSupplier")]
        public async Task<ActionResult<SupplierDTO>> Delete(int id)
        {
            var supplier = await _supplierService.GetSupplierById(id);
            if (supplier == null)
            {
                return NotFound("Supplier not found");
            }

            await _supplierService.Remove(id);

            return Ok(supplier);
        }
    }
}
