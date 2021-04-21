using DeliverIt.Services.Contracts;
using DeliverIt.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeliverIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;
        private readonly IAuthHelper authHelper; 
        public CitiesController(ICityService cityService, IAuthHelper authHelper)
        {
            this.cityService = cityService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Get all cities.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <returns>Returns all cities.</returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string authorizationUsername)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                return Ok(this.cityService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get a city by a certain ID.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID of the city to get.</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var city = this.cityService.Get(id);
                return Ok(city);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
