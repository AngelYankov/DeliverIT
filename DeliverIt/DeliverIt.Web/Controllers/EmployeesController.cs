using DeliverIt.Data.Models;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService; 
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.employeeService.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var employee = this.employeeService.Get(id);
                return Ok(employee);
            }
            catch (Exception)
            {
                return NotFound("There is no such employee.");
            }
        }

        [HttpPut("")]
        public IActionResult Create([FromBody] Employee model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var employee = this.employeeService.Create(model);
            return Created("post", employee);
        }

        [HttpPost("")]
        public IActionResult Update(int id, [FromBody] Employee model)
        {
            try
            {
                var employee = this.employeeService.Update(id, model);
                return Ok(employee);
            }
            catch (Exception)
            {
                return NotFound("There is no such employee.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = this.employeeService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("There is no such employee.");
            }
        }
    }
}
