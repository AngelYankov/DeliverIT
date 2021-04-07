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

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.cityService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var city = this.cityService.Get(id);
            if(city == null)
            {
                return NotFound("There is no such city");
            }

            return Ok(city);
        }
    }
}
