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
    public class CountryService : ICountryService
    {
        private readonly DeliverItContext dbContext;

        public CountryService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public CountryDTO Get(int id)
        {
            var country = this.dbContext.Countries.FirstOrDefault(c => c.Id == id);
            if(country == null)
            {
                throw new ArgumentNullException();
            }

            CountryDTO countryDTO = new CountryDTO(country);

            return countryDTO;
        }

        public IList<string> GetAll()
        {
            return dbContext.Countries.Select(c => c.Name).ToList();
        }
    }
}
