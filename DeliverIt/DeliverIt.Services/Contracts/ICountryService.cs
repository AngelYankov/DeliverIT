using DeliverIt.Services.Models;
using System.Collections.Generic;

namespace DeliverIt.Services.Contracts
{
    public interface ICountryService
    {
        CountryDTO Get(int id);
        IList<string> GetAll();
    }
}
