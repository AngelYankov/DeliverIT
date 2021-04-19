using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Return_All_Parcels()
        {
            var options = Utils.GetOptions(nameof(Return_All_Parcels));
            var parcels = Utils.SeedParcels();

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Parcels.AddRange(parcels);
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var result = sut.GetAll().ToList();

                Assert.AreEqual(parcels.Count, result.Count);
                Assert.AreEqual(string.Join(",", parcels.Select(p => new ParcelDTO(p))), string.Join(",", result));
            }
        }
    }
}
