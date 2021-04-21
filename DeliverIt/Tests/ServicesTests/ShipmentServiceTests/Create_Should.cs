using DeliverIt.Data;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.ShipmentServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Return_Created_Shipment()
        {
            var options = Utils.GetOptions(nameof(Return_Created_Shipment));

            var newShipmentDTO = new Mock<NewShipmentDTO>().Object;
            newShipmentDTO.Id = 1;
            newShipmentDTO.WarehouseId = 1;
            newShipmentDTO.StatusId = 1;

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
                var result = sut.Create(newShipmentDTO);

                Assert.AreEqual(1, actContext.Shipments.ToList().Count());
                Assert.AreEqual(newShipmentDTO.Id, result.Id);
                Assert.AreEqual(newShipmentDTO.StatusId, result.StatusId);
                Assert.AreEqual(newShipmentDTO.WarehouseId, result.WarehouseId);
            }
        }

        [TestMethod]
        public void Throws_When_InputShipmentWarehouseId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_InputShipmentWarehouseId_NotFound));

            var newShipmentDTO = new Mock<NewShipmentDTO>().Object;
            newShipmentDTO.Id = 1;
            newShipmentDTO.WarehouseId = 1;
            newShipmentDTO.StatusId = 1;

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Create(newShipmentDTO));
            }
        }

        [TestMethod]
        public void Throws_When_InputShipmentStatusId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_InputShipmentStatusId_NotFound));

            var newShipmentDTO = new Mock<NewShipmentDTO>().Object;
            newShipmentDTO.Id = 1;
            newShipmentDTO.WarehouseId = 1;
            newShipmentDTO.StatusId = 1;

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Create(newShipmentDTO));
            }
        }
    }
}
