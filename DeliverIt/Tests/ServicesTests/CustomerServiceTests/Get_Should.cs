using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.CustomerServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnCustomerById()
        {
            var options = Utils.GetOptions(nameof(ReturnCustomerById));
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Customers.AddRange(Utils.SeedCustomers());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new CustomerService(actContext, sutHelp);
                var result = sut.Get(1);
                var customer = actContext.Customers.FirstOrDefault(c => c.Id == 1);
                Assert.IsInstanceOfType(result, typeof(CustomerDTO));
                Assert.AreEqual(customer.FirstName, result.FirstName);
                Assert.AreEqual(customer.LastName, result.LastName);
                Assert.AreEqual(customer.Email, result.Email);
                Assert.AreEqual(customer.Address.StreetName + ", " + customer.Address.City.Name, result.Address);
            }
        }
        [TestMethod]
        public void Throw_When_InvalidCustomerId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidCustomerId));
            var mock = new Mock<IAddressService>();
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CustomerService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.Get(20));
            }
        }
    }
}
