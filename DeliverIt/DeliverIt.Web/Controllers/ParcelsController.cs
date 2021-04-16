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
    public class ParcelsController : ControllerBase
    {
        private readonly IParcelService parcelService;
        public ParcelsController(IParcelService parcelService)
        {
            this.parcelService = parcelService;
        }

        /// <summary>
        /// Create a parcel.
        /// </summary>
        /// <param name="model">Details of the parcel to be created.</param>
        /// <returns>Returns the created parcel or an appropriate error message.</returns>
        [HttpPost("")]
        public IActionResult Create([FromBody] NewParcelDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
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
        /// <returns>Returns all parcels.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.parcelService.GetAll());
        }

        /// <summary>
        /// Get a parcel by a certain ID.
        /// </summary>
        /// <param name="id">ID of the parcel to get.</param>
        /// <returns>Returns a parcel with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
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
        /// <param name="id">ID of the parcel to update.</param>
        /// <param name="model">Details of the parcel to be updated.</param>
        /// <returns>Returns the updated parcel or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateParcelDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            try
            {
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
        /// <param name="id">ID of the parcel to delete.</param>
        /// <returns>Returns no content or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
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
        /// <param name="filter1">weight/customer/warehouse/category</param>
        /// <param name="value1">Value of the first filter</param>
        /// <param name="filter2">weight/customer/warehouse/category</param>
        /// <param name="value2">Value of the second filter</param>
        /// <param name="sortBy1">weight/arrival</param>
        /// <param name="sortBy2">weight/arrival</param>
        /// <param name="sortingValue">asc/desc</param>
        /// <returns>Returns filtered and/or sorted parcels or an appropriate error message.</returns>
        [HttpGet("filtering&sorting")]
        public IActionResult GetBy([FromQuery] string filter1, string value1, string filter2, string value2, 
                                                                                              string sortBy1, string sortBy2, string sortingValue)
        {
            try
            {
                var parcelsDTO = this.parcelService.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, sortingValue);
                return Ok(parcelsDTO);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
