using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models
{
    public class CityDTO
    {
        public CityDTO(City city)
        {
            City = city.Name;
            Country = city.Country.Name;
        }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
