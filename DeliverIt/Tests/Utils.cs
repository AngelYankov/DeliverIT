using DeliverIt.Data;
using DeliverIt.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Utils
    {
        public static DbContextOptions<DeliverItContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<DeliverItContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;
        }

        public static IList<Parcel> SeedParcels()
        {
            return new List<Parcel>()
            {
                new Parcel()
                {
                    Id = 1,
                    CategoryId = 1,
                    CustomerId = 1,
                    ShipmentId = 1,
                    WarehouseId = 1,
                    Weight = 2.5
                },
                new Parcel()
                {
                    Id = 2,
                    CategoryId = 2,
                    CustomerId = 2,
                    ShipmentId = 2,
                    WarehouseId = 2,
                    Weight = 22.5
                }
            };
        }
        public static IList<Shipment> SeedShipments()
        {
            return new List<Shipment>()
            {
                new Shipment()
                {
                    Id = 1,
                    StatusId = 1,
                    WarehouseId = 1,
                    Departure = DateTime.UtcNow.AddDays(7),
                    Arrival = DateTime.UtcNow.AddDays(12)
                },
                new Shipment()
                {
                    Id = 2,
                    StatusId = 1,
                    WarehouseId = 2,
                    Departure = DateTime.UtcNow.AddDays(6),
                    Arrival = DateTime.UtcNow.AddDays(10)
                }
            };
        }
        public static IList<Warehouse> SeedWarehouses()
        {
            return new List<Warehouse>()
            {
                new Warehouse()
                {
                    Id = 1,
                    AddressId = 1
                },
                new Warehouse()
                {
                    Id = 2,
                    AddressId = 2
                }
            };
        }
        public static IList<Employee> SeedEmployees()
        {
            return new List<Employee>
            {
                new Employee()
                {
                    Id = 1,
                    FirstName = "Petar",
                    LastName = "Shapkov",
                    Email = "peter.shapkov@gmail.com",
                    AddressId = 1
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Eric",
                    LastName = "Berg",
                    Email = "eric.berg@gmail.com",
                    AddressId = 2
                }
            };
        }
        public static IList<Customer> SeedCustomers()
        {
            return new List<Customer>
            {
                new Customer()
                {
                    Id = 1,
                    FirstName = "Stefan",
                    LastName = "Popov",
                    Email = "stefan.popov@gmail.com",
                    AddressId = 1
                },
                new Customer()
                {
                    Id = 2,
                    FirstName = "Georgi",
                    LastName = "Ivanov",
                    Email = "georgi.ivanov@gmail.com",
                    AddressId = 2
                },
                new Customer()
                {
                    Id = 3,
                    FirstName = "Peter",
                    LastName = "Crouch",
                    Email = "peter.crouch@gmail.com",
                    AddressId = 3
                }
            };
        }
        public static IList<Address> SeedAddresses()
        {
            return new List<Address>
            {
                new Address()
                {
                    Id = 1,
                    StreetName = "Georgi Rakovski 1",
                    CityID = 1,
                },
                new Address()
                {
                    Id = 2,
                    StreetName = "Tsar Osvoboditel 10",
                    CityID = 2
                },
                new Address()
                {
                    Id = 3,
                    StreetName = "Mayfair Avenue 5",
                    CityID = 3
                }
            };
        }
        public static IList<Country> SeedCountries()
        {
            return new List<Country>
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
            };
        }
        public static IList<City> SeedCities()
        {
            return new List<City>
            {
                new City()
                {
                    Id = 1,
                    CountryId = 1,
                    Name = "Sofia"
                    
                },
                new City()
                {
                    Id = 2,
                    CountryId = 1,
                    Name = "Plovdiv"
                },
                new City()
                {
                    Id = 3,
                    CountryId = 2,
                    Name = "London"
                }
            };
        }
        public static IList<Category> SeedCategories()
        {
            return new List<Category>
            {
                new Category()
                {
                    Id = 1,
                    Name = "Electronics",
                },
                new Category()
                {
                    Id = 2,
                    Name = "Clothing",
                },
                new Category()
                {
                    Id = 3,
                    Name = "Medical"
                }
            };
        }
        public static IList<Status> SeedStatuses()
        {
            return new List<Status>
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
            };
        }
    }

}
