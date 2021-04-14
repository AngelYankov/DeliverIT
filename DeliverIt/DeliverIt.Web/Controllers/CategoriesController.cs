using DeliverIt.Data.Models;
using DeliverIt.Services.ModelsServices;
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
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("")]
        public IActionResult GetAll()
        {
            return Ok(this.categoryService.GetAll());
        }

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

        [HttpPut("{id}/{name}")]
        public IActionResult Update(int id, string name)
        {
            try
            {
                var category = this.categoryService.Update(id, name);
                return Ok(category);
            }
            catch (Exception)
            {
                return NotFound("There is no such category.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                bool deleted = this.categoryService.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest("There is no such category.");
            }
        }
    }
}
