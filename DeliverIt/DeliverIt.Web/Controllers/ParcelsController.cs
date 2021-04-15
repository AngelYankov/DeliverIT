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
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] NewParcelDTO model)
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

        [HttpGet("filtering")]
        public IActionResult GetBy([FromQuery] string filter,string value,string filter2, string value2)
        {
            try
            {
                var parcelsDTO = this.parcelService.GetBy(filter, value);
                return Ok(parcelsDTO);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
