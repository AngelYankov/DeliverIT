using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class CityService : ICityService
    {
        public City Get(int id)
        {
            return Database.Cities.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<City> GetAll()
        {
            return Database.Cities;
        }
    }
}
