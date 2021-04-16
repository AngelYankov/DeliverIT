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
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService; 
        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        /// <summary>
        /// Get all cities.
        /// </summary>
        /// <returns>Returns all cities.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.cityService.GetAll());
        }

        /// <summary>
        /// Get a city by a certain ID.
        /// </summary>
        /// <param name="id">ID of the city to get.</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var city = this.cityService.Get(id);
                return Ok(city);
            }
            catch (Exception)
            {
                return NotFound("There is no such city.");
            }
        }
    }
}
