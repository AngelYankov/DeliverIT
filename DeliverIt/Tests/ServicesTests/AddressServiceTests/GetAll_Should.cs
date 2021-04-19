using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.AddressServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void ReturnAllAddresses()
        {
            var options = Utils.GetOptions(nameof(ReturnAllAddresses));
            var addresses = Utils.SeedAddresses();

            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Addresses.AddRange(addresses);
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new AddressService(actContext);
                var result = sut.GetAll().ToList();
                Assert.AreEqual(addresses.Count, result.Count);
                Assert.AreEqual(string.Join(",", addresses.Select(a=>new AddressDTO(a))), string.Join(",", result));
            }
        }
    }
}
