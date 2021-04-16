using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
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
        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <returns>Returns all employees.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.employeeService.GetAll());
        }
        /// <summary>
        /// Get an employee by ID.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns employee with that ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var employee = this.employeeService.Get(id);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// Create new employee.
        /// </summary>
        /// <param name="model">Data to be created with.</param>
        /// <returns>Returns new employee or an appropriate error message.</returns>
        [HttpPost("")]
        public IActionResult Create([FromBody] NewEmployeeDTO model)
        {
            try
            {
                var employee = this.employeeService.Create(model);
                return Created("post", employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// <summary>
        /// Update an employee.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <param name="model">Data to be updated.</param>
        /// <returns>Returns updated employee or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateEmployeeDTO model)
        {
            try
            {
                var employee = this.employeeService.Update(id, model);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        /// <summary>
        /// Delete an employee.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns no content or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = this.employeeService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
