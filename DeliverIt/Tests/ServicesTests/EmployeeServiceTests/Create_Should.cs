using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.EmployeeServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnNewEmployee()
        {
            var options = Utils.GetOptions(nameof(ReturnNewEmployee));
            var newEmployeeDTO = new Mock<NewEmployeeDTO>().Object;
            newEmployeeDTO.FirstName = "John";
            newEmployeeDTO.LastName = "Smith";
            newEmployeeDTO.Email = "john.smith@gmail.com";
            newEmployeeDTO.AddressId = 1;
            var employee = new Mock<Employee>().Object;
            employee.FirstName = "John";
            employee.LastName = "Smith";
            employee.Email = "john.smith@gmail.com";
            employee.AddressId = 1;
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Employees.Add(employee);
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new EmployeeService(actContext, sutHelp);
                var result = sut.Create(newEmployeeDTO);
                Assert.AreEqual(employee.FirstName, result.FirstName);
                Assert.AreEqual(employee.LastName, result.LastName);
                Assert.AreEqual(employee.Email, result.Email);
                Assert.AreEqual(employee.Address.StreetName + ", " + employee.Address.City.Name, result.Address);
                Assert.AreEqual(actContext.Employees.Count(), 2);
                Assert.IsInstanceOfType(result, typeof(EmployeeDTO));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidAddress()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidAddress));
            var newEmployeeDTO = new Mock<NewEmployeeDTO>().Object;
            newEmployeeDTO.FirstName = "John";
            newEmployeeDTO.LastName = "Smith";
            newEmployeeDTO.Email = "john.smith@gmail.com";
            newEmployeeDTO.AddressId = 13;
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new EmployeeService(actContext, sutHelp);
                Assert.ThrowsException<ArgumentException>(() => sut.Create(newEmployeeDTO));
            }
        }
    }
}
