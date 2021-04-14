using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DeliverIt.Services.Models;

namespace DeliverIt.Services.Services
{

    public class CityService : ICityService
    {
        private readonly DeliverItContext dbContext;

        public CityService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public CityDTO Get(int id)
        {
            var city = FindCity(id);
            CityDTO cityDTO = new CityDTO(city);

            return cityDTO;
        }

        public IList<string> GetAll()
        {
            return dbContext.Cities.Select(c => c.Name).ToList();
        }

        private City FindCity(int id)
        {
            var city = this.dbContext.Cities
                                     .Include(c => c.Country)
                                     .FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                throw new ArgumentNullException();
            }
            return city;
        }
    }
}
