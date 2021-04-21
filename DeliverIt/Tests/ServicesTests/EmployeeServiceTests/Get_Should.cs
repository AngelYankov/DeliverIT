using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.EmployeeServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnEmployeeById()
        {
            var options = Utils.GetOptions(nameof(ReturnEmployeeById));
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Employees.AddRange(Utils.SeedEmployees());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new EmployeeService(actContext, sutHelp);
                var result = sut.Get(1);
                var customer = actContext.Employees.FirstOrDefault(c => c.Id == 1);
                Assert.IsInstanceOfType(result, typeof(EmployeeDTO));
                Assert.AreEqual(customer.FirstName, result.FirstName);
                Assert.AreEqual(customer.LastName, result.LastName);
                Assert.AreEqual(customer.Email, result.Email);
                Assert.AreEqual(customer.Address.StreetName + ", " + customer.Address.City.Name, result.Address);
            }
        }
        [TestMethod]
        public void Throw_When_InvalidEmployeeId()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidEmployeeId));
            var mock = new Mock<IAddressService>();
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new EmployeeService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.Get(20));
            }
        }
    }
}
