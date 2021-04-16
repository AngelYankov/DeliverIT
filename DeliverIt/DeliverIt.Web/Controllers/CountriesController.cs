using DeliverIt.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DeliverIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;
        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }

        /// <summary>
        /// Get all countries.
        /// </summary>
        /// <returns>Returns all countries.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.countryService.GetAll());
        }

        /// <summary>
        /// Get a country by a certain ID.
        /// </summary>
        /// <param name="id">ID of the country to get.</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var country = this.countryService.Get(id);
            if (country == null)
            {
                return NotFound("There is no such country.");
            }
            return Ok(country);
        }
    }
}
