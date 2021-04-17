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
    public class ParcelsController : ControllerBase
    {
        private readonly IParcelService parcelService;
        private readonly IAuthHelper authHelper;
        public ParcelsController(IParcelService parcelService, IAuthHelper authHelper)
        {
            this.parcelService = parcelService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Create a parcel.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="model">Details of the parcel to be created.</param>
        /// <returns>Returns the created parcel or an appropriate error message.</returns>
        [HttpPost("")]
        public IActionResult Create([FromHeader] string authorizationUsername, [FromBody] NewParcelDTO model)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var parcel = this.parcelService.Create(model);
                return Created("post", parcel);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get all parcels.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <returns>Returns all parcels.</returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string authorizationUsername)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                return Ok(this.parcelService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get a parcel by a certain ID.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID of the parcel to get.</param>
        /// <returns>Returns a parcel with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var parcel = this.parcelService.Get(id);
                return Ok(parcel);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Update a parcel.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID of the parcel to update.</param>
        /// <param name="model">Details of the parcel to be updated.</param>
        /// <returns>Returns the updated parcel or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromHeader] string authorizationUsername, int id, [FromBody] UpdateParcelDTO model)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var parcel = this.parcelService.Update(id, model);
                return Ok(parcel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Delete a parcel.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID of the parcel to delete.</param>
        /// <returns>Returns no content or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var parcel = this.parcelService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Filter and/or sort parcels.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="filter1">weight/customer/warehouse/category</param>
        /// <param name="value1">Value of the first filter</param>
        /// <param name="filter2">weight/customer/warehouse/category</param>
        /// <param name="value2">Value of the second filter</param>
        /// <param name="sortBy1">weight/arrival</param>
        /// <param name="sortBy2">weight/arrival</param>
        /// <param name="sortingValue">asc/desc</param>
        /// <returns>Returns filtered and/or sorted parcels or an appropriate error message.</returns>
        [HttpGet("filtering&sorting")]
        public IActionResult GetBy([FromHeader] string authorizationUsername, 
                                   [FromQuery] string filter1, string value1, string filter2, string value2, 
                                               string sortBy1, string sortBy2, string sortingValue)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var parcelsDTO = this.parcelService.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, sortingValue);
                return Ok(parcelsDTO);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Get the parcels of a certain customer
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="filter">past/future</param>
        /// <returns>Returns the past and/or future parcels of a certain customer or an appropriate error message.</returns>
        [HttpGet("customerParcels")]
        public IActionResult GetCustomerParcels([FromHeader] string authorizationUsername, string filter)
        {
            try
            {
                this.authHelper.TryGetCustomer(authorizationUsername);
                var customerParcels = this.parcelService.GetCustomerParcels(authorizationUsername, filter);
                return Ok(customerParcels);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
