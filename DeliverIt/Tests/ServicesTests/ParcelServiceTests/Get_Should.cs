using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void Return_Correct_Parcel()
        {
            var options = Utils.GetOptions(nameof(Return_Correct_Parcel));
            var parcels = Utils.SeedParcels();

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Parcels.AddRange(parcels);
                arrangeContext.SaveChanges();
            }

            var parcelDTO = new ParcelDTO(parcels.First());

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = sut.Get(1);

                Assert.AreEqual(parcelDTO.Id, result.Id);
                Assert.AreEqual(parcelDTO.Category, result.Category);
                Assert.AreEqual(parcelDTO.CustomerFirstName, result.CustomerFirstName);
                Assert.AreEqual(parcelDTO.CustomerLastName, result.CustomerLastName);
                Assert.AreEqual(parcelDTO.ParcelArrival, result.ParcelArrival);
                Assert.AreEqual(parcelDTO.WarehouseAddress, result.WarehouseAddress);
                Assert.AreEqual(parcelDTO.WarehouseCity, result.WarehouseCity);
                Assert.AreEqual(parcelDTO.Weight, result.Weight);
            }
        }

        [TestMethod]
        public void Throws_When_ParcelNotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_ParcelNotFound));
            using (var context = new DeliverItContext(options))
            {
                var sut = new ParcelService(context);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Get(1));
            }
        }
    }
}
