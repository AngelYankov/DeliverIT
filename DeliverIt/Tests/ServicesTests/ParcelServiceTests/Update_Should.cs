using DeliverIt.Data;
using DeliverIt.Services.Models.Update;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void Return_Updated_Parcel()
        {
            var options = Utils.GetOptions(nameof(Return_Updated_Parcel));

            var updateParcelDTO = new UpdateParcelDTO()
            {
                CustomerId = 1,
                ShipmentId = 1,
                WarehouseId = 1,
                CategoryId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = sut.Update(1, updateParcelDTO);

                Assert.AreEqual(updateParcelDTO.CustomerId, result.CustomerId);
                Assert.AreEqual(updateParcelDTO.WarehouseId, result.WarehouseId);
                Assert.AreEqual(updateParcelDTO.CategoryId, result.CategoryId);
                Assert.AreEqual(updateParcelDTO.Weight, result.Weight);
                Assert.AreEqual(updateParcelDTO.ShipmentId, result.ShipmentId);
            }
        }

        [TestMethod]
        public void Throws_When_UpdateInputCategoryId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateInputCategoryId_NotFound));

            var updateParcelDTO = new UpdateParcelDTO()
            {
                CustomerId = 1,
                ShipmentId = 1,
                WarehouseId = 1,
                CategoryId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
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

                Assert.ThrowsException<ArgumentNullException>(() => sut.Update(1, updateParcelDTO));
            }
        }

        [TestMethod]
        public void Throws_When_UpdateInputShipmentId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateInputShipmentId_NotFound));

            var updateParcelDTO = new UpdateParcelDTO()
            {
                CustomerId = 1,
                ShipmentId = 1,
                WarehouseId = 1,
                CategoryId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
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

                Assert.ThrowsException<ArgumentNullException>(() => sut.Update(1, updateParcelDTO));
            }
        }

        [TestMethod]
        public void Throws_When_UpdateInputWarehouseId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateInputWarehouseId_NotFound));

            var updateParcelDTO = new UpdateParcelDTO()
            {
                CustomerId = 1,
                ShipmentId = 1,
                WarehouseId = 1,
                CategoryId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Update(1, updateParcelDTO));
            }
        }

        [TestMethod]
        public void Throws_When_UpdateInputCustomerId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateInputCustomerId_NotFound));

            var updateParcelDTO = new UpdateParcelDTO()
            {
                CustomerId = 1,
                ShipmentId = 1,
                WarehouseId = 1,
                CategoryId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Update(1, updateParcelDTO));
            }
        }
    }
}
