using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
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
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] NewWarehouseDTO model)
        {
            try
            {
                var warehouse = this.warehouseService.Update(id, model);
                return Ok(warehouse);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("")]
        public IActionResult Create([FromBody] NewWarehouseDTO model)
        {
            try
            {
                var warehouse = this.warehouseService.Create(model);
                return Created("post", warehouse);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.warehouseService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
