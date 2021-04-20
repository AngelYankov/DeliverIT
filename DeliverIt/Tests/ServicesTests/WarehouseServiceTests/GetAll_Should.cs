using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.WarehouseServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void ReturnAllWarehouses()
        {
            var options = Utils.GetOptions(nameof(ReturnAllWarehouses));
            var mockService = new Mock<IAddressService>();

            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new WarehouseService(actContext, mockService.Object);
                var result = sut.GetAll();
                Assert.AreEqual(actContext.Warehouses.Count(), result.Count());
                Assert.AreEqual(string.Join(",",actContext.Warehouses.Select(w => new WarehouseDTO(w))), string.Join(",", result));
            }
        }
    }
}
