using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void Returns_Created_Parcel()
        {
            var options = Utils.GetOptions(nameof(Returns_Created_Parcel));

            var parcel = new Parcel()
            {
                Id = 1,
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 1
            };
            var newParcelDTO = new NewParcelDTO()
            {
                CategoryId = 1,
                CustomerId = 1,
                WarehouseId = 1,
                ShipmentId = 1,
                Weight = 1
            };

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Add(parcel);
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
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
                var result = sut.Create(newParcelDTO);

                Assert.AreEqual(2, actContext.Parcels.ToList().Count());
                Assert.AreEqual(newParcelDTO.CustomerId, result.CustomerId);
                Assert.AreEqual(newParcelDTO.CategoryId, result.CategoryId);
                Assert.AreEqual(newParcelDTO.ShipmentId, result.ShipmentId);
                Assert.AreEqual(newParcelDTO.WarehouseId, result.WarehouseId);
            }
        }
    }
}
