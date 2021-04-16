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

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.parcelService.GetAll());
        }

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
        /// Filtering and sorting of the parcels
        /// </summary>
        /// <param name="filter1">"First property to filter parcels by"</param>
        /// <param name="value1">Value of the first filter</param>
        /// <param name="filter2">Second property to filter parcels by</param>
        /// <param name="value2">Value of the second filter</param>
        /// <param name="sortBy1">First property to sort by</param>
        /// <param name="sortBy2">Second property to sort by</param>
        /// <param name="sortingValue">Value to sort by</param>
        /// <returns>Filtered and/or sorted parcels</returns>
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
