using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models
{
    public class WarehouseDTO
    {
        public WarehouseDTO(Warehouse warehouse)
        {
            this.Address = warehouse.Address.StreetName;
            this.City = warehouse.Address.City.Name;
        }
        public string Address { get; set; }
        public string City { get; set; }
    }
}
