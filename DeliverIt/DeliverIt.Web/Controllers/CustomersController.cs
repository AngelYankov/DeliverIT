using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.customerService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var customer = this.customerService.Get(id);
                return Ok(customer);
            }
            catch (Exception)
            {
                return NotFound("There is no such customer.");
            }
        }
        [HttpPost("")]
        public IActionResult Create([FromBody] NewCustomerDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            var customer = this.customerService.Create(model);
            return Created("post", customer);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] UpdateCustomerDTO model)
        {
            try
            {
                var customer = this.customerService.Update(id,model);
                return Ok(customer);
            }
            catch (Exception)
            {
                return NotFound("There is no such customer.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.customerService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("There is no such customer.");
            }
        }
    }
}
