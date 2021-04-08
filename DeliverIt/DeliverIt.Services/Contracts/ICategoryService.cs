using DeliverIt.Data.Models;
using System.Collections.Generic;

namespace DeliverIt.Services.ModelsServices
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category Create(Category category);
        Category Update(int id, string name);
        bool Delete(int id);
    }
}