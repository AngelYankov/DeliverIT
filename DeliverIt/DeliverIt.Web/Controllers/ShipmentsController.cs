using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
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
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService shipmentService;
        public ShipmentsController(IShipmentService shipmentService)
        {
            this.shipmentService = shipmentService;
        }

        /// <summary>
        /// Create a shipment.
        /// </summary>
        /// <param name="model">Details of the shipment to be created.</param>
        /// <returns>Returns the created shipment or an appropriate error message.</returns>
        [HttpPost("")]
        public IActionResult Create([FromBody] NewShipmentDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                var shipment = this.shipmentService.Create(model);
                return Created("post", shipment);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get all shipments.
        /// </summary>
        /// <returns>Returns all shipments.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.shipmentService.GetAll());
        }

        /// <summary>
        /// Get a shipment by a certain ID.
        /// </summary>
        /// <param name="id">ID of the shipment to get.</param>
        /// <returns>Returns a shipment with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var shipment = this.shipmentService.Get(id);
                return Ok(shipment);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Update a shipment.
        /// </summary>
        /// <param name="id">ID of the shipment to be updated.</param>
        /// <param name="model">Details of the shipment to be updated.</param>
        /// <returns>Returns the updated shipment or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateShipmentDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
                var shipment = this.shipmentService.Update(id, model);
                return Ok(shipment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a shipment.
        /// </summary>
        /// <param name="id">ID of the shipment to be deleted.</param>
        /// <returns>Returns response code and an appropriate message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var shipment = this.shipmentService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Filter shipments.
        /// </summary>
        /// <param name="filter">warehouse/customer</param>
        /// <param name="value">id/first_name or last_name</param>
        /// <returns>Returns filtered shipments or an appropriate error message.</returns>
        [HttpGet("filtering")]
        public IActionResult GetBy([FromQuery] string filter, string value)
        {
            try
            {
                var shipmentsDTO = this.shipmentService.GetBy(filter, value);
                return Ok(shipmentsDTO);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
