using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
                arrContext.Addresses.Add(address);
                arrContext.AddRange(Utils.SeedCities());
            }
            var addressDTO = new AddressDTO(address);
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                var result = sut.Create(newAddressDTO);
                Assert.AreEqual(addressDTO.Id, result.Id);
                Assert.AreEqual(addressDTO.StreetAddress, result.StreetAddress);
                Assert.AreEqual(addressDTO.Address.City.Id, result.Address.City.Id);
            }
            //todo
        }
    }
}
