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

        /// <summary>
        /// Get a city by a certain ID.
        /// </summary>
        /// <param name="id">ID of the city to get.</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        public CityDTO Get(int id)
        {
            var city = FindCity(id);
            CityDTO cityDTO = new CityDTO(city);

            return cityDTO;
        }

        /// <summary>
        /// Get all cities.
        /// </summary>
        /// <returns>Returns all cities.</returns>
        public IList<string> GetAll()
        {
            return dbContext.Cities.Select(c => c.Name).ToList();
        }

        /// <summary>
        /// Find a city with certain ID.
        /// </summary>
        /// <param name="id">ID of the city to find.</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        private City FindCity(int id)
        {
            var city = this.dbContext.Cities
                                     .Include(c => c.Country)
                                     .FirstOrDefault(c => c.Id == id)
                                     ?? throw new ArgumentNullException(Exceptions.InvalidCity);

            return city;
        }
    }
}
