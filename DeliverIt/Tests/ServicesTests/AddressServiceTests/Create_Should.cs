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

namespace Tests.ServicesTests.AddressServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnCreatedCustomer()
        {
            var options = Utils.GetOptions(nameof(ReturnCreatedCustomer));
            var newAddressDTO = new NewAddressDTO()
            {
                StreetName = "Ivan Vazov",
                CityId = 1,
            };
            var address = new Address()
            {
                StreetName = "Ivan Vazov",
                CityID = 1,
                CreatedOn = DateTime.UtcNow
            };
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.AddRange(Utils.SeedCities());
                arrContext.Addresses.Add(address);
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                var result = sut.Create(newAddressDTO);
                Assert.AreEqual(address.StreetName, result.Address.StreetName);
                Assert.AreEqual(address.City.Id, result.Address.City.Id);
                Assert.IsInstanceOfType(result, typeof(AddressDTO));
                Assert.AreEqual(actContext.Addresses.Count(), 2);
                Assert.IsTrue(address.City.Addresses.Contains(address));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidCity()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidCity));
            var newAddressDTO = new NewAddressDTO()
            {
                StreetName = "Ivan Vazov",
                CityId = 100,
            };
            
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                Assert.ThrowsException<ArgumentException>(() => sut.Create(newAddressDTO));
            }
        }
    }
}
