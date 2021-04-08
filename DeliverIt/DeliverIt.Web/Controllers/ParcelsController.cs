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
    public class ParcelsController : ControllerBase
    {
        private readonly IParcelService parcelService;
        public ParcelsController(IParcelService parcelService)
        {
            this.parcelService = parcelService;
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
            catch (Exception)
            {
                return NotFound("There is no such parcel.");
            }
        }
    }
}
