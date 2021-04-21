using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.WarehouseServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnCreatedWarehouse()
        {
            var options = Utils.GetOptions(nameof(ReturnCreatedWarehouse));

            var newWarehouseDTO = new Mock<NewWarehouseDTO>().Object;
            newWarehouseDTO.AddressId = 1;
            var warehouse = new Mock<Warehouse>().Object;
            warehouse.AddressId = 1;
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.Warehouses.Add(warehouse);
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var address = actContext.Addresses.Include(a => a.City).FirstOrDefault(a => a.Id == 1);
                var mockService = new Mock<IAddressService>();
                mockService.Setup(a => a.Get(It.IsAny<int>())).Returns(new AddressDTO(address));
                var sut = new WarehouseService(actContext, mockService.Object);
                var result = sut.Create(newWarehouseDTO);
                Assert.AreEqual(warehouse.Address.StreetName + ", " + warehouse.Address.City.Name, result.Address);
                Assert.IsInstanceOfType(result, typeof(WarehouseDTO));
                Assert.IsTrue(actContext.Warehouses.Contains(warehouse));
            }
        }
    }
}
