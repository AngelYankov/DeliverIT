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
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.countryService.GetAll());
        }
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
