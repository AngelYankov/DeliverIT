using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.CustomerServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void ReturnAllCustomers()
        {
            var options = Utils.GetOptions(nameof(ReturnAllCustomers));
            var mock = new Mock<IAddressService>();
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Customers.AddRange(Utils.SeedCustomers());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options)) 
            {
                var sut = new CustomerService(actContext, mock.Object);
                var result = sut.GetAll();
                Assert.AreEqual(actContext.Customers.Count(), result.Count());
                Assert.AreEqual(string.Join(", ", actContext.Customers.Select(c => new CustomerDTO(c))), string.Join(", ", result));
            }
        }
    }
}
