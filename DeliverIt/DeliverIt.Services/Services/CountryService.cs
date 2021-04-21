using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIt.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly DeliverItContext dbContext;

        public CountryService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Get a country by a certain ID.
        /// </summary>
        /// <param name="id">ID of the country to get</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        public CountryDTO Get(int id)
        {
            var country = FindCountry(id);
            CountryDTO countryDTO = new CountryDTO(country);

            return countryDTO;
        }

        /// <summary>
        /// Get all countries.
        /// </summary>
        /// <returns>Returns all countries.</returns>
        public IList<string> GetAll()
        {
            return dbContext.Countries.Select(c => c.Name).ToList();
        }

        /// <summary>
        /// Find a country by a certain ID.
        /// </summary>
        /// <param name="id">ID of the country to get.</param>
        /// <returns>Returns a city with certain ID or an appropriate error message.</returns>
        private Country FindCountry(int id)
        {
            var country = this.dbContext.Countries.FirstOrDefault(c => c.Id == id)
                ??throw new ArgumentNullException(Exceptions.InvalidCountry);

            return country;
        }
    }
}
