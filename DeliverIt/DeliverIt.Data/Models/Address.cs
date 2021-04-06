﻿using DeliverIt.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Address : IAddress
    {
        public int Id { get; set; }

        public string StreetName { get; set; }

        public int CityID { get; set; }

        public City City { get; set; }

        public HashSet<Employee> Employees { get; set; }

        public HashSet<Customer> Customers { get; set; }

        public HashSet<Warehouse> Warehouses { get; set; }
    }
}
