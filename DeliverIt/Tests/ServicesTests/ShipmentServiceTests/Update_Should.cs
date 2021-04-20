using DeliverIt.Data;
using DeliverIt.Services.Models.Update;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ServicesTests.ShipmentServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void Return_Updated_Shipment()
        {
            var options = Utils.GetOptions(nameof(Return_Updated_Shipment));

            var updateShipmentDTO = new UpdateShipmentDTO()
            {
                WarehouseId = 1,
                StatusId = 1,
                Departure = DateTime.UtcNow.AddDays(1),
                Arrival = DateTime.UtcNow.AddDays(2)
            };

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
                var result = sut.Update(1, updateShipmentDTO);

                Assert.AreEqual(updateShipmentDTO.StatusId, result.StatusId);
                Assert.AreEqual(updateShipmentDTO.WarehouseId, result.WarehouseId);
                Assert.AreEqual(updateShipmentDTO.Departure.ToString("dd.MMMM.yyyy"), result.Departure);
                Assert.AreEqual(updateShipmentDTO.Arrival.ToString("dd.MMMM.yyyy"), result.Arrival);
            }
        }

        [TestMethod]
        public void Throws_When_UpdateShipmentInputWarehouseId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateShipmentInputWarehouseId_NotFound));

            var updateShipmentDTO = new UpdateShipmentDTO()
            {
                WarehouseId = 1,
                StatusId = 1,
                Departure = DateTime.UtcNow.AddDays(1),
                Arrival = DateTime.UtcNow.AddDays(2)

            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Statuses.AddRange(Utils.SeedStatuses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Update(1, updateShipmentDTO));
            }
        }

        [TestMethod]
        public void Throws_When_UpdateShipmentInputStatusId_NotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_UpdateShipmentInputStatusId_NotFound));

            var updateShipmentDTO = new UpdateShipmentDTO()
            {
                WarehouseId = 1,
                StatusId = 1,
                Departure = DateTime.UtcNow.AddDays(1),
                Arrival = DateTime.UtcNow.AddDays(2)

            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ShipmentService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Update(1, updateShipmentDTO));
            }
        }
    }
}
