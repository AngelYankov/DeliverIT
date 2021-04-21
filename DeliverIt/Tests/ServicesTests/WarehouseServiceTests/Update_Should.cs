using DeliverIt.Data;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.WarehouseServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void ReturnUpdatedWarehouse()
        {
            var options = Utils.GetOptions(nameof(ReturnUpdatedWarehouse));
            var newWarehouseDTO = new Mock<NewWarehouseDTO>().Object;
            newWarehouseDTO.AddressId = 3;
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new WarehouseService(actContext, sutHelp);
                var warehouse = actContext.Warehouses.FirstOrDefault(w => w.Id == 2);
                var result = sut.Update(2, newWarehouseDTO);
                Assert.AreEqual(warehouse.Address.StreetName + ", " + warehouse.Address.City.Name, result.Address);
            }
        }
        [TestMethod]
        public void Throw_When_InvalidWarehouseId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidWarehouseId));
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new WarehouseService(actContext, sutHelp);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(5, new NewWarehouseDTO()));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidAddressId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidAddressId));
            var newWarehouseDTO = new Mock<NewWarehouseDTO>().Object;
            newWarehouseDTO.AddressId = 1;
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new WarehouseService(actContext, sutHelp);
                var warehouse = actContext.Warehouses.FirstOrDefault(w => w.Id == 2);
                var address = actContext.Addresses.FirstOrDefault(a => a.Id == 1);
                address.Warehouse = warehouse;
                Assert.ThrowsException<ArgumentException>(() => sut.Update(2, newWarehouseDTO));
            }
        }
    }
}
