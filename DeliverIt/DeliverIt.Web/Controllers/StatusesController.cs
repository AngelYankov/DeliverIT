using DeliverIt.Services.Contracts;
using DeliverIt.Web.Helpers;
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
        private readonly IAuthHelper authHelper;
        public StatusesController(IStatusService statusService, IAuthHelper authHelper)
        {
            this.statusService = statusService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Get all statuses.
        /// </summary>
        /// <param name="authorizationUsername">Username to authorize user.</param>
        /// <returns>Returns all statuses.</returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string authorizationUsername)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                return Ok(this.statusService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get a status by a certain ID.
        /// </summary>
        /// <param name="authorizationUsername">Username to authorize user.</param>
        /// <param name="id">ID of the status to get.</param>
        /// <returns>Returns a status with certain ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
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
