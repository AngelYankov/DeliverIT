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

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.shipmentService.GetAll());
        }

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

        [HttpGet("filter")]
        public IActionResult GetBy([FromQuery] int warehouseId)
        {
            var shipmentsDTO = this.shipmentService.GetBy(warehouseId);
            return Ok(shipmentsDTO);
        }
    }
}
