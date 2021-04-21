using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.WarehouseServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void ReturnsTrueIfDeleted()
        {
            var options = Utils.GetOptions(nameof(ReturnsTrueIfDeleted));
            var mock = new Mock<IAddressService>();
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new WarehouseService(actContext, mock.Object);
                var result = sut.Delete(1);
                Assert.AreEqual(actContext.Warehouses.Where(w => w.IsDeleted == false).Count(), 1);
                Assert.IsTrue(result);
            }
        }
        [TestMethod]
        public void Throw_When_InvalidWarehouseId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidWarehouseId));
            var mock = new Mock<IAddressService>();
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new WarehouseService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.Delete(5));
            }
        }
        [TestMethod]
        public void Throw_When_AlreadyDeleted()
        {
            var options = Utils.GetOptions(nameof(Throw_When_AlreadyDeleted));
            var mock = new Mock<IAddressService>();
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new WarehouseService(actContext, mock.Object);
                var warehouse = actContext.Warehouses.First(w => w.Id == 1);
                warehouse.IsDeleted = true;
                Assert.ThrowsException<ArgumentException>(() => sut.Delete(1));
            }
        }

    }
}
