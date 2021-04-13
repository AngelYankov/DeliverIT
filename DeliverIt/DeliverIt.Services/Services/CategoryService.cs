using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.ModelsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DeliverItContext dbContext;

        public CategoryService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Category Create(Category category)
        {
            dbContext.Categories.Add(category);
            category.CreatedOn = DateTime.UtcNow;
            return category;
        }
        public IList<string> GetAll()
        {
            return dbContext.Categories.Where(c=>c.IsDeleted==false).Select(c => c.Name).ToList();
        }
        public Category Update(int id, string name)
        {
            var category = FindCategory(id);
            category.Name = name;
            category.ModifiedOn = DateTime.UtcNow;
            return category;
        }
        public bool Delete(int id)
        {
            var category = FindCategory(id);
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            return category.IsDeleted;
        }
        private Category FindCategory(int id)
        {
            var category = dbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            {
                throw new ArgumentNullException();
            }
            if (category.IsDeleted)
            {
                throw new ArgumentException();
            }
            return category;
        }
    }
}
