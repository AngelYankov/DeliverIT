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
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService addressService;
        public AddressesController(IAddressService addressService)
        {
            this.addressService = addressService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.addressService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var address = this.addressService.Get(id);
                return Ok(address);
            }
            catch (Exception)
            {
                return NotFound("There is no such address.");
            }
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] Address address)
        {
            if (address == null)
            {
                return BadRequest();
            }
            this.addressService.Create(address);
            return Created("post", address);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] Address address)
        {
            try
            {
                this.addressService.Update(id, address);
                return Ok(address);
            }
            catch (Exception)
            {
                return NotFound("There is no such address.");
            }
        }
    }
}
