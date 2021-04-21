using DeliverIt.Data;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.AddressServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnsAddressById()
        {
            var options = Utils.GetOptions(nameof(ReturnsAddressById));

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                var result = sut.Get(1);
                var address = actContext.Addresses.FirstOrDefault(a => a.Id == 1);
                Assert.AreEqual(address.StreetName+","+address.City.Name, result.StreetAddress);
                Assert.AreEqual(address.Id, result.Id);
                Assert.AreEqual(address.Id, result.Address.Id);
            }
        }
        [TestMethod]
        public void Throw_When_AddressNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_AddressNotFound));
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                Assert.ThrowsException<ArgumentException>(() => sut.Get(1));
            }
        }
    }
}
