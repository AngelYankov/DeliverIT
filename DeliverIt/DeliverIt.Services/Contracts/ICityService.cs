using DeliverIt.Services.Models;
using System.Collections.Generic;

namespace DeliverIt.Services.Contracts
{
    public interface ICityService
    {
        CityDTO Get(int id);
        IList<string> GetAll();
    }
}
