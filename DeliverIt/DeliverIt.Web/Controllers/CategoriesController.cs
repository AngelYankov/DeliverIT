using DeliverIt.Services.ModelsServices;
using DeliverIt.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeliverIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        private readonly IAuthHelper authHelper;

        public CategoriesController(ICategoryService categoryService, IAuthHelper authHelper)
        {
            this.categoryService = categoryService;
            this.authHelper = authHelper;
        }

        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="name">Name of new category.</param>
        /// <returns>Returns new category or an appropriate error message.</returns>
        [HttpPost("{name}")]
        public IActionResult Create([FromHeader] string authorizationUsername, string name)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                if (name == null)
                {
                    return BadRequest();
                }
                var category = this.categoryService.Create(name);
                return Created("post", category);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <returns>Returns all categories.</returns>
        [HttpGet("")]
        public IActionResult GetAll([FromHeader] string authorizationUsername)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                return Ok(this.categoryService.GetAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Update existing category by ID.
        /// </summary>
        /// <param name="authorizationUsername">Username to validate.</param>
        /// <param name="id">ID to search for.</param>
        /// <param name="name">New name of category.</param>
        /// <returns>Returns updated category or an appropriate error message.</returns>
        [HttpPut("{id}/{name}")]
        public IActionResult Update([FromHeader] string authorizationUsername, int id, string name)
        {
            try
            {
                this.authHelper.TryGetEmployee(authorizationUsername);
                var category = this.categoryService.Update(id, name);
                return Ok(category);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Delete a category.
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
                this.categoryService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
