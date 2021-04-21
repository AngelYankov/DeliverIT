using DeliverIt.Data;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests.ServicesTests.ShipmentServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void Delete_Selected_Shipment()
        {
            var options = Utils.GetOptions(nameof(Delete_Selected_Shipment));

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);
                var result = sut.Delete(1);

                Assert.IsTrue(result);
            }
        }

        [TestMethod]
        public void Throws_When_ShipmentNotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_ShipmentNotFound));

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Delete(1));
            }
        }
    }
}
