using DeliverIt.Data;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void Delete_Selected_Parcel()
        {
            var options = Utils.GetOptions(nameof(Delete_Selected_Parcel));

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = sut.Delete(1);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void Throws_When_ParcelNotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_ParcelNotFound));

            using (var arrangeContext = new DeliverItContext(options))
            {
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

                Assert.ThrowsException<ArgumentNullException>(() => sut.Delete(1));
            }
        }
    }
}
