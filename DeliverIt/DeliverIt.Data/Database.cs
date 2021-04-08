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
            SeedStatuses();
            SeedCategories();
        }
       public static List<Address> Addresses { get; set; }
       public static List<Category> Categories { get; set; }
       public static List<City> Cities { get; set; }
       public static List<Country> Countries { get; set; }
       public static List<Customer> Customers { get; set; }
       public static List<Employee> Employees { get; set; }
       public static List<Parcel> Parcels{ get; set; }
       public static List<Shipment> Shipments{ get; set; }
       public static List<Status> Statuses{ get; set; }
       public static List<Warehouse> Warehouses{ get; set; }

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
        private static void SeedStatuses()
        {
            Statuses.AddRange(new List<Status>
            {
                new Status()
                {
                    Id=1,
                    Name = "Preparing",
                },
                new Status()
                {
                    Id=2,
                    Name = "On the way",
                },
                new Status()
                {
                    Id=3,
                    Name = "Completed"
                }
            });
        }
        private static void SeedCategories()
        {
            Categories.AddRange(new List<Category>
            {
                new Category()
                {
                    Id=1,
                    Name = "Electronics",
                },
                new Category()
                {
                    Id=2,
                    Name = "Clothing",
                },
                new Category()
                {
                    Id=3,
                    Name = "Medical"
                }
            });
        }
    }
}
