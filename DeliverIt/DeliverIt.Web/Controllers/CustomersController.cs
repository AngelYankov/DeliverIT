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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.customerService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var customer = this.customerService.Get(id);
                return Ok(customer);
            }
            catch (Exception)
            {
                return NotFound("There is no such customer.");
            }
        }
    }
}
