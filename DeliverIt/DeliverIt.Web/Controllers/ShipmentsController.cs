using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
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
            catch (Exception)
            {
                return NotFound("There is no such shipment.");
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
