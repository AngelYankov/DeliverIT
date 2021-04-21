using DeliverIt.Data;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.ShipmentServiceTests
{
    [TestClass]
    public class GetAllCount_Should
    {
        [TestMethod]
        public void Return_Correct_ShipmentCount()
        {
            var options = Utils.GetOptions(nameof(Return_Correct_ShipmentCount));
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
                var count = actContext.Shipments.Where(s => s.IsDeleted == false && s.Arrival > DateTime.UtcNow).Count();
                var result = sut.GetAllCount();

                Assert.AreEqual(count, result);
            }
        }
    }
}
