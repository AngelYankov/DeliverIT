using DeliverIt.Services.Contracts;
using DeliverIt.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeliverIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;
        private readonly IAuthHelper authHelper;

        public CountriesController(ICountryService countryService, IAuthHelper authHelper)
        {
            this.countryService = countryService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Get all countries.
        /// </summary>
        /// <param name="authorizationUsername">Username to authorize user.</param>
        /// <returns>Returns all countries.</returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string authorizationUsername)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                return Ok(this.countryService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get a country by a certain ID.
        /// </summary>
        /// <param name="authorizationUsername">Username to authorize user.</param>
        /// <param name="id">ID of the country to get.</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var country = this.countryService.Get(id);
                return Ok(country);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
