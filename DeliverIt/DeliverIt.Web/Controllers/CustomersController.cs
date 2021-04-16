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
        /// <summary>
        /// Get all customers.
        /// </summary>
        /// <returns>Returns all customers.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.customerService.GetAll());
        }
        /// <summary>
        /// Get customer by ID
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns customer with that ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var customer = this.customerService.Get(id);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// Create new customer.
        /// </summary>
        /// <param name="model">Data of new customer.</param>
        /// <returns>Returns created customer or an appropriate error message.</returns>
        [HttpPost("")]
        public IActionResult Create([FromBody] NewCustomerDTO model)
        {
            try
            {
                var customer = this.customerService.Create(model);
                return Created("post", customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        /// <summary>
        /// Update existing customer's data.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <param name="model">Data to be updated.</param>
        /// <returns>Returns updated customer or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] UpdateCustomerDTO model)
        {
            try
            {
                var customer = this.customerService.Update(id,model);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// Delete a customer.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns no contect or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                this.customerService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
