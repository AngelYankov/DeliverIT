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
        private readonly DeliverItContext dbContext;

        public CityService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string Get(int id)
        {
            var city = dbContext.Cities.FirstOrDefault(c => c.Id == id).Name;
            if(city == null)
            {
                throw new ArgumentNullException();
            }
            return city;
        }

        public IList<string> GetAll()
        {
            return dbContext.Cities.Select(c => c.Name).ToList();
        }
    }
}
