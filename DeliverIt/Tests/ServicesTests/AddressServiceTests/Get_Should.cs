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

namespace Tests.ServicesTests.AddressServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnsAddressById()
        {
            var options = Utils.GetOptions(nameof(ReturnsAddressById));
            var addresses = Utils.SeedAddresses();

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Addresses.AddRange(addresses);
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }
            var addressDTO = new AddressDTO(addresses.First());
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                var result = sut.Get(1);
                Assert.AreEqual(addressDTO.StreetAddress, result.StreetAddress);
                Assert.AreEqual(addressDTO.Id, result.Id);
                Assert.AreEqual(addressDTO.Address.Id, result.Address.Id);
            }
        }
        [TestMethod]
        public void Throw_When_AddressNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_AddressNotFound));
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                Assert.ThrowsException<ArgumentException>(() =>sut.Get(1));
            }
        }
    }
}
