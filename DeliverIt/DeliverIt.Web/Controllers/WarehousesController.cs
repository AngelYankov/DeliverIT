using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IWarehouseService warehouseService;
        public WarehousesController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.warehouseService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var warehouse = this.warehouseService.Get(id);
                return Ok(warehouse);
            }
            catch (Exception)
            {
                return NotFound("There is no such warehouse.");
            }
        }
        [HttpPost("{id}")]
        public IActionResult Update(int id, [FromBody] Warehouse model)
        {
            try
            {
                this.warehouseService.Update(id,model);
                return Ok(model);
            }
            catch (Exception)
            {
                return NotFound("There is no such warehouse.");
            }
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] Warehouse model)
        {
            if (model==null)
            {
                return BadRequest();
            }
            var warehouse = this.warehouseService.Create(model);
            return Created("post", warehouse);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.warehouseService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("There is no such warehouse.");
            }
        }
    }
}
