using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class GetCustomerParcels_Should
    {
        [TestMethod]
        public void Return_CertainCustomerParcels_Future()
        {
            var options = Utils.GetOptions(nameof(Return_CertainCustomerParcels_Future));
            string username = "Georgi.Ivanov";
            string filter = "future";

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
                var filtered = actContext.Parcels.Where(p => ((p.Customer.FirstName + "." + p.Customer.LastName) == username) && p.Shipment.Arrival >= DateTime.UtcNow);
                var result = sut.GetCustomerParcels(username, filter);

                Assert.AreEqual(string.Join(",", filtered.Select(f=> new ParcelDTO(f))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Return_CertainCustomerParcels_All()
        {
            var options = Utils.GetOptions(nameof(Return_CertainCustomerParcels_All));
            string username = "Georgi.Ivanov";
            string filter = null;

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var filtered = actContext.Parcels.Where(p => (p.Customer.FirstName + "." + p.Customer.LastName) == username);
                var result = sut.GetCustomerParcels(username, filter);

                Assert.AreEqual(string.Join(",", filtered.Select(f => new ParcelDTO(f))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Throws_When_ParcelsNotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_ParcelsNotFound));
            string username = "test";
            string filter = "test";

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.GetCustomerParcels(username, filter));
            }
        }
    }
}
