using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
using DeliverIt.Web.Helpers;
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
        private readonly IAuthHelper authHelper;

        public CustomersController(ICustomerService customerService, IAuthHelper authHelper)
        {
            this.customerService = customerService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Get all customers count.
        /// </summary>
        /// <returns>Returns number of customers.</returns>
        [HttpGet("customerCount")]
        public IActionResult GetCount()
        {
            var count = this.customerService.GetAllCount();
            return Ok(count);
        }

        /// <summary>
        /// Get all customers.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <returns>Returns all customers.</returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string authorizationUsername)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                return Ok(this.customerService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Get customer by ID.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns customer with that ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
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
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <param name="model">Data to be updated.</param>
        /// <returns>Returns updated customer or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromHeader] string authorizationUsername, int id, [FromBody] UpdateCustomerDTO model)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var customer = this.customerService.Update(id, model);
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
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns no contect or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
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
