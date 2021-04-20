using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.ShipmentServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Return_AllShipments()
        {
            var options = Utils.GetOptions(nameof(Return_AllShipments));
            var shipments = Utils.SeedShipments();
            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Shipments.AddRange(shipments);
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);
                var result = sut.GetAll();

                Assert.AreEqual(shipments.Count, result.ToList().Count);
                Assert.AreEqual(string.Join(",", shipments.Select(p => new ShipmentDTO(p))), string.Join(",", result));
            }
        }
    }
}
