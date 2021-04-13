﻿using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models
{
    public class ParcelDTO
    {
        public ParcelDTO(Parcel parcel)
        {
            CustomerFirstName = parcel.Customer.FirstName;
            CustomerLastName = parcel.Customer.LastName;
            WarehouseAddress = parcel.Warehouse.Address.StreetName;
            WarehouseCity = parcel.Warehouse.Address.City.Name;
            Category = parcel.Category.Name;
            Weight = parcel.Weight;
        }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string WarehouseAddress { get; set; }
        public string WarehouseCity { get; set; }
        public string Category { get; set; }
        public double Weight { get; set; }
    }
}