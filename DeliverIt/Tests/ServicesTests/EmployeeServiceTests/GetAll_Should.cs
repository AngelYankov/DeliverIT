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
    public class GetAll_Should
    {
        [TestMethod]
        public void ReturnAllEmployees()
        {
            var options = Utils.GetOptions(nameof(ReturnAllEmployees));
            var mock = new Mock<IAddressService>();
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Employees.AddRange(Utils.SeedEmployees());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new EmployeeService(actContext, mock.Object);
                var result = sut.GetAll();
                Assert.AreEqual(actContext.Employees.Count(), result.Count());
                Assert.AreEqual(string.Join(", ", actContext.Employees.Select(c => new EmployeeDTO(c))), string.Join(", ", result));
            }
        }
    }
}
