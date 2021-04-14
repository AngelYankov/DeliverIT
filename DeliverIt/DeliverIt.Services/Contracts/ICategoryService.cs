using DeliverIt.Data.Models;
using System.Collections.Generic;

namespace DeliverIt.Services.ModelsServices
{
    public interface ICategoryService
    {
        IList<string> GetAll();
        string Create(string categoryName);
        string Update(int id, string name);
        bool Delete(int id);
    }
}