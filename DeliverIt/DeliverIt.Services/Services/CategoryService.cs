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

        public string Create(string categoryName)
        {
            var category = new Category();
            category.Name = categoryName;
            category.CreatedOn = DateTime.UtcNow;
            this.dbContext.Categories.Add(category);
            this.dbContext.SaveChanges();
            return category.Name;
        }
        public IList<string> GetAll()
        {
            return this.dbContext.Categories.Where(c=>c.IsDeleted==false).Select(c => c.Name).ToList();
        }
        public string Update(int id, string name)
        {
            var category = FindCategory(id);
            category.Name = name;
            category.ModifiedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return category.Name;
        }
        public bool Delete(int id)
        {
            var category = FindCategory(id);
            category.IsDeleted = true;
            category.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return category.IsDeleted;
        }
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
