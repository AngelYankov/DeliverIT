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
        /// <summary>
        /// Get all warehouses.
        /// </summary>
        /// <returns>Returns all warehouses.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.warehouseService.GetAll());
        }
        /// <summary>
        /// Get warehouse by ID.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns warehouse with that ID or an appropriate error message.</returns>
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
        /// <summary>
        /// Update certain warehouse data.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <param name="model">Data to be updated.</param>
        /// <returns>Returns updated warehouse or an appropriate error message.</returns>
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
        /// <summary>
        /// Create a warehouse.
        /// </summary>
        /// <param name="model">Data of warehouse to be created with.</param>
        /// <returns>Returns created warehouse or an appropriate error message.</returns>
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
        /// <summary>
        /// Delete a warehouse.
        /// </summary>
        /// <param name="id">ID of warehouse to search for.</param>
        /// <returns>Returns no content or an appropriate error message.</returns>
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
