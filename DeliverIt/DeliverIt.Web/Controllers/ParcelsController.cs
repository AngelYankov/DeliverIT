using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models.Create;
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
            catch (Exception)
            {
                return NotFound("There is no such parcel.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Parcel model)
        {
            try
            {
                var parcel = this.parcelService.Update(id, model);
                return Ok(parcel);
            }
            catch (Exception)
            {
                return BadRequest();
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
            catch (Exception)
            {
                return NotFound("There is no such parcel.");
            }
        }

        [HttpGet("filter")]
        public IActionResult GetBy([FromQuery] string filter, string value)
        {
            try
            {
                var parcelsDTO = this.parcelService.GetBy(filter, value);
                return Ok(parcelsDTO);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
