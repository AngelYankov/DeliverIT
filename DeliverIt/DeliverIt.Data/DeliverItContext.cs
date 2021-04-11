using DeliverIt.Data.Models;
using Microsoft.EntityFrameworkCore;
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
        public DbSet<ShipmentWarehouse> ShipmentWarehouses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
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
                    Id=1,
                    CountryId=1,
                    Name = "Sofia",
                },
                new City()
                {
                    Id=2,
                    CountryId=2,
                    Name = "London",
                },
                new City()
                {
                    Id=3,
                    CountryId = 3,
                    Name = "Stockholm"
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
            };
            modelBuilder.Entity<Country>().HasData(countries);
            modelBuilder.Entity<City>().HasData(cities);
            modelBuilder.Entity<Status>().HasData(statuses);
            modelBuilder.Entity<Category>().HasData(categories);
            #region
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Warehouse)
                .WithMany(w => w.Parcels)
                .HasForeignKey(p => p.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Parcels)
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Parcels)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Shipment)
                .WithMany(s => s.Parcels)
                .HasForeignKey(p => p.ShipmentId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
            #region
            modelBuilder.Entity<ShipmentWarehouse>()
                .HasKey(sw => new { sw.ShipmentId, sw.WarehouseId });
            modelBuilder.Entity<ShipmentWarehouse>()
                .HasOne(sw => sw.Shipment)
                .WithMany(sw => sw.ShipmentWarehouses)
                .HasForeignKey(sw => sw.ShipmentId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ShipmentWarehouse>()
                .HasOne(sw => sw.Warehouse)
                .WithMany(sw => sw.ShipmentWarehouses)
                .HasForeignKey(sw => sw.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
