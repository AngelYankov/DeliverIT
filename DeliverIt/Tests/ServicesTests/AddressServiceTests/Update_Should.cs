using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.AddressServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void ReturnUpdatedAddress()
        {
            var options = Utils.GetOptions(nameof(ReturnUpdatedAddress));
            var newAddressDTO = new NewAddressDTO()
            {
                StreetName = "Orel",
                CityId = 1,
                WarehouseId=1
            };

            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                var result = sut.Update(1,newAddressDTO);
                var address = actContext.Addresses.FirstOrDefault(a => a.Id == 1);
                Assert.AreEqual(address.Id, result.Id);
                Assert.AreEqual(address.StreetName, result.Address.StreetName);
                Assert.AreEqual(address.CityID, result.Address.City.Id);
                Assert.AreEqual(address.Warehouse.Id, result.Address.Warehouse.Id);
                Assert.IsInstanceOfType(result, typeof(AddressDTO));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidCityId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidCityId));
            var newAddressDTO = new NewAddressDTO()
            {
                StreetName = "Orel",
                CityId = 100,
                WarehouseId = 1
            };
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(1, newAddressDTO));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidAddressId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidAddressId));
            var newAddressDTO = new NewAddressDTO()
            {
                StreetName = "Orel",
                CityId = 1,
                WarehouseId = 1
            };
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(100, newAddressDTO));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidWarehouseId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidWarehouseId));
            var newAddressDTO = new NewAddressDTO()
            {
                StreetName = "Orel",
                CityId = 1,
                WarehouseId = 100
            };
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(1, newAddressDTO));
            }
        }

    }
}
