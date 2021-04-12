using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models
{
    public class AddressDTO
    {
       
        public AddressDTO(Address address)
        {
            this.StreetName = address.StreetName;
            this.CityName = address.City.Name;
        }
        public string StreetName { get; set; }
        public string CityName { get; set; }
        
    }
}
