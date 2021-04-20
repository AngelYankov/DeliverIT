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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.WarehouseServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void ReturnUpdatedWarehouse()
        {
            var options = Utils.GetOptions(nameof(ReturnUpdatedWarehouse));
            var mockService = new Mock<IAddressService>();
            var newWarehouseDTO = new NewWarehouseDTO()
            {
                AddressId = 3
            };
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
                var result = sut.Update(1, newWarehouseDTO);
                var warehouse = actContext.Warehouses.First(w => w.Id == 1);
                Assert.AreEqual(warehouse.Address.StreetName + ", " + warehouse.Address.City.Name, result.Address);
            }
            //TODO
        }
    }
}
