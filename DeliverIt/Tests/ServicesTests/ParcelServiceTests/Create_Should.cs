using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Return_Created_Parcel()
        {
            var options = Utils.GetOptions(nameof(Return_Created_Parcel));

            var newParcelDTO = new NewParcelDTO()
            {
                Id = 1,
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = sut.Create(newParcelDTO);

                Assert.AreEqual(1, actContext.Parcels.ToList().Count());
                Assert.AreEqual(newParcelDTO.Id, result.Id);
                Assert.AreEqual(newParcelDTO.CustomerId, result.CustomerId);
                Assert.AreEqual(newParcelDTO.CategoryId, result.CategoryId);
                Assert.AreEqual(newParcelDTO.ShipmentId, result.ShipmentId);
                Assert.AreEqual(newParcelDTO.WarehouseId, result.WarehouseId);
            }
        }

        [TestMethod]
        public void Throws_When_InputParcelCustomerId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_InputParcelCustomerId_NotFound));

            var newParcelDTO = new NewParcelDTO()
            {
                Id = 1,
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Create(newParcelDTO));
            }
        }

        [TestMethod]
        public void Throws_When_InputParcelWarehouseId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_InputParcelWarehouseId_NotFound));

            var newParcelDTO = new NewParcelDTO()
            {
                Id = 1,
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Create(newParcelDTO));
            }
        }

        [TestMethod]
        public void Throws_When_InputParcelCategoryId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_InputParcelCategoryId_NotFound));

            var newParcelDTO = new NewParcelDTO()
            {
                Id = 1,
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Create(newParcelDTO));
            }
        }

        [TestMethod]
        public void Throws_When_InputParcelShipmentId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_InputParcelShipmentId_NotFound));

            var newParcelDTO = new NewParcelDTO()
            {
                Id = 1,
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Create(newParcelDTO));
            }
        }

        [TestMethod]
        public void Throws_When_InputParcelWeight_NotValid()
        {
            var options = Utils.GetOptions(nameof(Throws_When_InputParcelWeight_NotValid));

            var newParcelDTO = new NewParcelDTO()
            {
                Id = 1,
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 0
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentOutOfRangeException>(() => sut.Create(newParcelDTO));
            }
        }
    }
}
