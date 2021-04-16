﻿using DeliverIt.Services.Contracts;
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
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] NewAddressDTO address)
        {
            
            try
            {
                this.addressService.Create(address);
                return Created("post", address);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
