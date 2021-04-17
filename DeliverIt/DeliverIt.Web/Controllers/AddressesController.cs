using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models.Create;
using DeliverIt.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeliverIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressService addressService;
        private readonly IAuthHelper authHelper;

        public AddressesController(IAddressService addressService, IAuthHelper authHelper)
        {
            this.addressService = addressService;
            this.authHelper = authHelper;
        }
        /// <summary>
        /// Get all addresses.
        /// </summary>
        /// <returns>Returns all addresses.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.addressService.GetAll());
        }
        /// <summary>
        /// Get certain address.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns address with that ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var address = this.addressService.Get(id);
                return Ok(address);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// Create a new address
        /// </summary>
        /// <param name="model">Data of new address.</param>
        /// <returns>Returns created address or an appropriate error message.</returns>
        [HttpPost("")]
        public IActionResult Create([FromBody] NewAddressDTO model)
        {

            try
            {
                var address = this.addressService.Create(model);
                return Created("post", address);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        /// <summary>
        /// Update certain address by ID
        /// </summary>
        /// <param name="authorizationUsername">Username authorization</param>
        /// <param name="id">ID to search for.</param>
        /// <param name="model">Data to be updated with.</param>
        /// <returns>Returns updated address or an appropriate error message</returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromHeader] string authorizationUsername, int id, [FromBody] NewAddressDTO model)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var address = this.addressService.Update(id, model);
                return Ok(address);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
