using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface ICountryService
    {
        Country Get(int id);
        IEnumerable<Country> GetAll();
    }
}
