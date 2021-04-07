using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;

namespace DeliverIt.Data
{
    public static class Database 
    {
        static Database()
        {
            Addresses = new List<Address>();
            Categories = new List<Category>();
            Cities = new List<City>();
            Countries = new List<Country>();
            Customers = new List<Customer>();
            Employees = new List<Employee>();
            Parcels = new List<Parcel>();
            Shipments = new List<Shipment>();
            Statuses = new List<Status>();
            Warehouses = new List<Warehouse>();
            SeedCountries();
            SeedCities();
        }
        static List<Address> Addresses { get; set; }
        static List<Category> Categories { get; set; }
        static List<City> Cities { get; set; }
        static List<Country> Countries { get; set; }
        static List<Customer> Customers { get; set; }
        static List<Employee> Employees { get; set; }
        static List<Parcel> Parcels{ get; set; }
        static List<Shipment> Shipments{ get; set; }
        static List<Status> Statuses{ get; set; }
        static List<Warehouse> Warehouses{ get; set; }

        private static void SeedCountries()
        {
            Countries.AddRange(new List<Country>
            {
                new Country()
                {
                    Id=1,
                    Name = "Bulgaria",
                },
                new Country()
                {
                    Id=2,
                    Name = "United Kingdom",
                },
                new Country()
                {
                    Id=3,
                    Name = "Sweden" 
                }
            });
        }
        private static void SeedCities()
        {
            Cities.AddRange(new List<City>
            {
                new City()
                {
                    Id=1,
                    Name = "Sofia",
                },
                new City()
                {
                    Id=2,
                    Name = "London",
                },
                new City()
                {
                    Id=3,
                    Name = "Stockholm"
                }
            });
        }

    }
}
