using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.ShipmentServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void Return_Correct_Shipment()
        {
            var options = Utils.GetOptions(nameof(Return_Correct_Shipment));
            var shipments = Utils.SeedShipments();
            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Shipments.AddRange(shipments);
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }
            var shipmentDTO = new ShipmentDTO(shipments.First());

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);
                var result = sut.Get(1);

                Assert.AreEqual(shipmentDTO.Id, result.Id);
                Assert.AreEqual(shipmentDTO.Status, result.Status);
                Assert.AreEqual(shipmentDTO.Warehouse, result.Warehouse);
                Assert.AreEqual(shipmentDTO.Departure, result.Departure);
                Assert.AreEqual(shipmentDTO.Arrival, result.Arrival);
            }
        }

        [TestMethod]
        public void Throws_When_ShipmentNotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_ShipmentNotFound));

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Get(1));
            }
        }
    }
}
