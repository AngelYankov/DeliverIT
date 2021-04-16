using DeliverIt.Services.ModelsServices;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DeliverIt.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>Returns all categories.</returns>
        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.categoryService.GetAll());
        }
        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="name">Name of new category.</param>
        /// <returns>Returns new category or an appropriate error message.</returns>
        [HttpPost("{name}")]
        public IActionResult Create(string name)
        {
            if(name == null)
            {
                return BadRequest();
            }

            var category = this.categoryService.Create(name);
            return Created("post",category);
        }
        /// <summary>
        /// Update existing category by ID.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <param name="name">New name of category.</param>
        /// <returns>Returns updated category or an appropriate error message.</returns>
        [HttpPut("{id}/{name}")]
        public IActionResult Update(int id, string name)
        {
            try
            {
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
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns no content or an appropriate error message.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
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
