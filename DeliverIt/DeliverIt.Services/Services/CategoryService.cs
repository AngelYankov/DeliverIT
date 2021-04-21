using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.ModelsServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIt.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DeliverItContext dbContext;

        public CategoryService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Create a category.
        /// </summary>
        /// <param name="categoryName">The name of the category.</param>
        /// <returns>Returns the name of the created category or an appropriate error message.</returns>
        public string Create(string categoryName)
        {
            var category = new Category();
            category.Name = categoryName;
            category.CreatedOn = DateTime.UtcNow;
            this.dbContext.Categories.Add(category);
            this.dbContext.SaveChanges();
            return category.Name;
        }
        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns>Returns all names of the categories</returns>
        public IList<string> GetAll()
        {
            return this.dbContext.Categories.Where(c=>c.IsDeleted==false).Select(c => c.Name).ToList();
        }
        /// <summary>
        /// Update name of category.
        /// </summary>
        /// <param name="id">ID of the category to be updated.</param>
        /// <param name="name">Name it should be updated to.</param>
        /// <returns>Returns the name of the updated category.</returns>
        public string Update(int id, string name)
        {
            var category = FindCategory(id);
            category.Name = name;
            category.ModifiedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return category.Name;
        }
        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id">ID of the category.</param>
        /// <returns>Returns true or false if category is deleted succesfully or an appropriate error message. </returns>
        public bool Delete(int id)
        {
            var category = FindCategory(id);
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return category.IsDeleted;
        }
        /// <summary>
        /// Finds a category
        /// </summary>
        /// <param name="id">ID of category to search for</param>
        /// <returns>Returns category with that ID or an appropriate error message.</returns>
        private Category FindCategory(int id)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Id == id)
                                    ?? throw new ArgumentException(Exceptions.InvalidCategory);
            if (category.IsDeleted)
            {
                throw new ArgumentException(Exceptions.DeletedCategory);
            }
            return category;
        }
    }
}
