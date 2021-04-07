using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class CountryService : ICountryService
    {
        public Country Get(int id)
        {
            return Database.Countries.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Country> GetAll()
        {
            return Database.Countries;
        }
    }
}
