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

        /// <summary>
        /// Get all statuses.
        /// </summary>
        /// <returns>Returns all statuses.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.statusService.GetAll());
        }

        /// <summary>
        /// Get a status by a certain ID.
        /// </summary>
        /// <param name="id">ID of the status to get.</param>
        /// <returns>Returns a status with certain ID or an appropriate error message.</returns>
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
