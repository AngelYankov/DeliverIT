using DeliverIt.Data.Models;
using System.Collections.Generic;

namespace DeliverIt.Services.ModelsServices
{
    public interface ICategoryService
    {
        Category Get(int id);
        IEnumerable<Category> GetAll(); 
    }
}