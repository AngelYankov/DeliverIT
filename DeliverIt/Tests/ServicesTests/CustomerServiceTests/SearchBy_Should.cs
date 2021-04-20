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

namespace Tests.ServicesTests.CustomerServiceTests
{
    [TestClass]
    public class SearchBy_Should
    {
        [TestMethod]
        public void ReturnFilteredCustomers()
        {
            var options = Utils.GetOptions(nameof(ReturnFilteredCustomers));
            string filter = "firstName";
            string value = "stefan";
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Customers.AddRange(Utils.SeedCustomers());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
                var result = sut.SearchBy(filter, value, null);
                var filtered = actContext.Customers
                    .Where(c => c.FirstName.Equals(value, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(c => c.FirstName);
                Assert.AreEqual(string.Join(", ",filtered.Select(c=>new CustomerDTO(c))),string.Join(", ", result));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidFilters()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidFilters));
            string filter = "a";
            string value = "1";
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.SearchBy(filter, value, null));
            }
        }
        [TestMethod]
        public void ReturnFilteredCustomersDesc()
        {
            var options = Utils.GetOptions(nameof(ReturnFilteredCustomersDesc));
            string filter = "lastName";
            string value = "popov";
            string order = "desc";
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
                var result = sut.SearchBy(filter, value, order);
                var filtered = actContext.Customers
                    .Where(c => c.LastName.Equals(value, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(c => c.LastName);
                Assert.AreEqual(string.Join(", ", filtered.Select(c => new CustomerDTO(c))), string.Join(", ", result));
            }
        }
    }
}
