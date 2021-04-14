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
            catch (Exception)
            {
                return NotFound("There is no such warehouse.");
            }
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] NewWarehouseDTO model)
        {
            try
            {
                this.warehouseService.Update(id, model);
                return Ok(model);
            }
            catch (Exception)
            {
                return NotFound("There is no such warehouse.");
            }
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] NewWarehouseDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                var warehouse = this.warehouseService.Create(model);
                return Created("post", warehouse);
            }
            catch (Exception)
            {
                return BadRequest();
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
            catch (Exception)
            {
                return BadRequest("There is no such warehouse.");
            }
        }
    }
}
