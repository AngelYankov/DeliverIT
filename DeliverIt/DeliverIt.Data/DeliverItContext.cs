using DeliverIt.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;

namespace DeliverIt.Data
{
    public class DeliverItContext : DbContext
    {
        public DeliverItContext(DbContextOptions<DeliverItContext> options)
            : base(options)
        {
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);

            #region
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Warehouse)
                .WithMany(w => w.Parcels)
                .HasForeignKey(p => p.WarehouseId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Parcels)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Parcels)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Shipment)
                .WithMany(s => s.Parcels)
                .HasForeignKey(p => p.ShipmentId)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion

            modelBuilder.Entity<Address>()
                        .HasOne(a => a.Warehouse)
                        .WithOne(w => w.Address)
                        .HasForeignKey<Warehouse>(w => w.AddressId);

            base.OnModelCreating(modelBuilder);
        }

        private static void SeedData(ModelBuilder modelBuilder)
        {
            var countries = new List<Country>
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
            var cities = new List<City>
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
                },
                new City()
                {
                    Id = 4,
                    CountryId = 2,
                    Name = "Birmingham"
                },
                new City()
                {
                    Id = 5,
                    CountryId = 3,
                    Name = "Stockholm"
                },
                new City()
                {
                    Id = 6,
                    CountryId = 3,
                    Name = "Malmo"
                }
            };
            var statuses = new List<Status>
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
            var categories = new List<Category>
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
            var addresses = new List<Address>
            {
                new Address()
                {
                    Id = 1,
                    StreetName = "Georgi Rakovski 1",
                    CityID = 1
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
                },
                new Address()
                {
                    Id = 4,
                    StreetName = "Wayton Road 3",
                    CityID = 4
                },
                new Address()
                {
                    Id = 5,
                    StreetName = "Central Park 23",
                    CityID = 5
                },
                new Address()
                {
                    Id = 6,
                    StreetName = "Central Ave 34",
                    CityID = 6
                },
                new Address()
                {
                    Id = 7,
                    StreetName = "Dondukov blvd 23",
                    CityID = 1
                },
                new Address()
                {
                    Id = 8,
                    StreetName = "Picadilly Circus 13",
                    CityID = 2
                },
                new Address()
                {
                    Id = 9,
                    StreetName = "Sickla Kanalgata 3",
                    CityID = 3
                },
                new Address()
                {
                    Id = 10,
                    StreetName = "Industrialna zona 2",
                    CityID = 1
                },
                new Address()
                {
                    Id = 11,
                    StreetName = "Industrial Park 53",
                    CityID = 2
                },
                new Address()
                {
                    Id = 12,
                    StreetName = "Vasatan 7",
                    CityID = 3
                },
            };
            var customers = new List<Customer>
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
                },
                new Customer()
                {
                    Id = 4,
                    FirstName = "Steven",
                    LastName = "Tyler",
                    Email = "steven.tyler@gmail.com",
                    AddressId = 4
                },
                new Customer()
                {
                    Id = 5,
                    FirstName = "Sven",
                    LastName = "Jorgensson",
                    Email = "sven.jorgensson@gmail.com",
                    AddressId = 5
                },
                new Customer()
                {
                    Id = 6,
                    FirstName = "Ragnar",
                    LastName = "Lothbrok",
                    Email = "ragnar.lothbrok@gmail.com",
                    AddressId = 6
                },
            };
            var employees = new List<Employee>
            {
                new Employee()
                {
                    Id = 1,
                    FirstName = "Petar",
                    LastName = "Shapkov",
                    Email = "peter.shapkov@gmail.com",
                    AddressId = 7
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Tyler",
                    LastName = "Johnson",
                    Email = "tyler.johnson@gmail.com",
                    AddressId = 8
                },
                new Employee()
                {
                    Id = 3,
                    FirstName = "Eric",
                    LastName = "Berg",
                    Email = "eric.berg@gmail.com",
                    AddressId = 9
                },
            };
            var warehouses = new List<Warehouse>()
            {
                new Warehouse()
                {
                    Id = 1,
                    AddressId = 10
                },
                new Warehouse()
                {
                    Id = 2,
                    AddressId = 11
                },
                new Warehouse()
                {
                    Id = 3,
                    AddressId = 12
                }
            };
            var shipments = new List<Shipment>()
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
                },
                new Shipment()
                {
                    Id = 3,
                    StatusId = 1,
                    WarehouseId = 3,
                    Departure = DateTime.UtcNow.AddDays(10),
                    Arrival = DateTime.UtcNow.AddDays(14)
                }
            };
            var parcels = new List<Parcel>()
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
                },
                new Parcel()
                {
                    Id = 3,
                    CategoryId = 3,
                    CustomerId = 3,
                    ShipmentId = 3,
                    WarehouseId = 3,
                    Weight = 1
                }
            };

            modelBuilder.Entity<Country>().HasData(countries);
            modelBuilder.Entity<City>().HasData(cities);
            modelBuilder.Entity<Status>().HasData(statuses);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Address>().HasData(addresses);
            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<Warehouse>().HasData(warehouses);
            modelBuilder.Entity<Shipment>().HasData(shipments);
            modelBuilder.Entity<Parcel>().HasData(parcels);
        }
    }
}
