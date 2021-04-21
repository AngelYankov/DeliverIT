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
                var result = sut.SearchBy(filter, value,null,null, null);
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
                Assert.ThrowsException<ArgumentException>(() => sut.SearchBy(filter, value,null,null, null));
            }
        }
        [TestMethod]
        public void ReturnFilteredCustomersDesc()
        {
            var options = Utils.GetOptions(nameof(ReturnFilteredCustomersDesc));
            string filter = "lastName";
            string value = "popov";
            string filter2 = "firstName";
            string value2 = "stefan";
            string order = "desc";
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
                var result = sut.SearchBy(filter, value,filter2,value2, order);
                var filtered = actContext.Customers
                    .Where(c => c.IsDeleted == false && c.LastName.Equals(value, StringComparison.OrdinalIgnoreCase) && c.FirstName.Equals(value2, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(c => c.LastName).ThenByDescending(c=>c.FirstName);
                Assert.AreEqual(string.Join(", ", filtered.Select(c => new CustomerDTO(c))), string.Join(", ", result));
            }
        }
    }
}
