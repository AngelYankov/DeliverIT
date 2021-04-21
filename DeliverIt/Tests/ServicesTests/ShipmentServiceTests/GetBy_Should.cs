using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.ShipmentServiceTests
{
    [TestClass]
    public class GetBy_Should
    {
        [TestMethod]
        public void Return_Shipments_Warehouse3()
        {
            var options = Utils.GetOptions(nameof(Return_Shipments_Warehouse3));
            string filter = "warehouse";
            string value = "1";

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
                var filtered = actContext.Shipments.Where(s => s.WarehouseId == int.Parse(value) && s.IsDeleted == false);
                var result = sut.GetBy(filter, value);

                Assert.AreEqual(string.Join(",", filtered.Select(p => new ShipmentDTO(p))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Throws_When_Invalid_ShipmentFilter()
        {
            var options = Utils.GetOptions(nameof(Throws_When_Invalid_ShipmentFilter));
            string filter = "test";
            string value = "1";

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

                Assert.ThrowsException<ArgumentNullException>(() => sut.GetBy(filter, value));
            }
        }

        [TestMethod]
        public void Throws_When_Invalid_ShipmentFilterValue()
        {
            var options = Utils.GetOptions(nameof(Throws_When_Invalid_ShipmentFilterValue));
            string filter = "warehouse";
            string value = "0";

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

                Assert.ThrowsException<ArgumentNullException>(() => sut.GetBy(filter, value));
            }
        }
    }
}
