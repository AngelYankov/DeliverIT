using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly IAuthHelper authHelper;

        public EmployeesController(IEmployeeService employeeService, IAuthHelper authHelper)
        {
            this.employeeService = employeeService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Create new employee.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="model">Data to be created with.</param>
        /// <returns>Returns new employee or an appropriate error message.</returns>
        [HttpPost("")]
        public IActionResult Create([FromHeader] string authorizationUsername, [FromBody] NewEmployeeDTO model)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var employee = this.employeeService.Create(model);
                return Created("post", employee);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <returns>Returns all employees.</returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string authorizationUsername)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var employees = this.employeeService.GetAll();
                return Ok(employees);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get an employee by ID.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns employee with that ID or an appropriate error message.</returns>
        [HttpGet("{id}")]
        public IActionResult Get([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var employee = this.employeeService.Get(id);
                return Ok(employee);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        
        /// <summary>
        /// Update an employee.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <param name="model">Data to be updated.</param>
        /// <returns>Returns updated employee or an appropriate error message.</returns>
        [HttpPut("{id}")]
        public IActionResult Update([FromHeader] string authorizationUsername, int id, [FromBody] UpdateEmployeeDTO model)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
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
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns no content or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete([FromHeader] string authorizationUsername, int id)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var employee = this.employeeService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Filter and sort employees.
        /// </summary>
        /// <param name="authorizationName">Username to validate.</param>
        /// <param name="filter">firstName/lastName/email</param>
        /// <param name="value">Value of the filter.</param>
        /// <param name="order">asc/desc</param>
        /// <returns></returns>
        [HttpGet("filtering&sorting")]
        public IActionResult GetBy([FromHeader] string authorizationName, [FromQuery] string filter, string value, string order)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationName);
                var filtered = this.employeeService.SearchBy(filter, value, order);
                return Ok(filtered);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
