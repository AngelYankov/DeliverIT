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

namespace Tests.ServicesTests.EmployeeServiceTests
{
    [TestClass]
    public class SearchBy_Should
    {
        [TestMethod]
        public void ReturnFilteredEmployee()
        {
            var options = Utils.GetOptions(nameof(ReturnFilteredEmployee));
            string filter = "firstName";
            string value = "petar";
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Employees.AddRange(Utils.SeedEmployees());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new EmployeeService(actContext, mock.Object);
                var result = sut.SearchBy(filter, value, null);
                var filtered = actContext.Employees
                    .Where(c => c.FirstName.Equals(value, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(c => c.FirstName);
                Assert.AreEqual(string.Join(", ", filtered.Select(c => new EmployeeDTO(c))), string.Join(", ", result));
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
                var sut = new EmployeeService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.SearchBy(filter, value, null));
            }
        }
        [TestMethod]
        public void ReturnFilteredEmployeesDesc()
        {
            var options = Utils.GetOptions(nameof(ReturnFilteredEmployeesDesc));
            string filter = "lastName";
            string value = "shapkov";
            string order = "desc";
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new EmployeeService(actContext, mock.Object);
                var result = sut.SearchBy(filter, value, order);
                var filtered = actContext.Employees
                    .Where(c => c.LastName.Equals(value, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(c => c.LastName);
                Assert.AreEqual(string.Join(", ", filtered.Select(c => new EmployeeDTO(c))), string.Join(", ", result));
            }
        }
    }
}
