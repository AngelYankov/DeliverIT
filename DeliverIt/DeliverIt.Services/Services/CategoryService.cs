using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.ModelsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    class CategoryService : ICategoryService
    {
        public Category Create(Category category)
        {
            Database.Categories.Add(category);
            category.CreatedOn = DateTime.UtcNow;
            return category;
        }
        public IEnumerable<Category> GetAll()
        {
            return Database.Categories;
        }
        public Category Update(int id, string name)
        {
            var category = Database.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new ArgumentNullException();
            }

            category.Name = name;
            category.ModifiedOn = DateTime.UtcNow;

            return category;
        }
        public bool Delete(int id)
        {
            var category = Database.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                throw new ArgumentNullException();
            }

            category.IsDeleted = Database.Categories.Remove(category);

            if (category.IsDeleted)
            {
                category.DeletedOn = DateTime.UtcNow;
            }
            return category.IsDeleted;
        }
    }
}
