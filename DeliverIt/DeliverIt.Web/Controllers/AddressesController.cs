using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models.Create;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult Create([FromBody] NewAddressDTO address)
        {
            if (address == null)
            {
                return BadRequest();
            }
            try
            {
                this.addressService.Create(address);
                return Created("post", address);
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] NewAddressDTO address)
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
