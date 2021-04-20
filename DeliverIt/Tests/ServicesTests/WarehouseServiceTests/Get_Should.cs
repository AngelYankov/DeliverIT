using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.WarehouseServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnWarehouseById()
        {
            var options = Utils.GetOptions(nameof(ReturnWarehouseById));
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
                var sut = new WarehouseService(actContext,mockService.Object);
                var result = sut.Get(1);
                var warehouse = actContext.Warehouses.FirstOrDefault(w => w.Id == 1);
                Assert.AreEqual(warehouse.Address.StreetName + ", " + warehouse.Address.City.Name, result.Address);
                Assert.IsInstanceOfType(result, typeof(WarehouseDTO));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidId));
            var mockService = new Mock<IAddressService>();
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new WarehouseService(actContext, mockService.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.Get(1));
            }
        }
    }
}
