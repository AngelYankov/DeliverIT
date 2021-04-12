using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models
{
    public class CountryDTO
    {
        public CountryDTO(Country country)
        {
            Name = country.Name;
        }
        public string Name { get; set; }
    }
}
