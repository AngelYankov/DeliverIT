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
    public class StatusesController : ControllerBase
    {
        private readonly IStatusService statusService;
        public StatusesController(IStatusService statusService)
        {
            this.statusService = statusService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.statusService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var status = this.statusService.Get(id);
                return Ok(status);

            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
