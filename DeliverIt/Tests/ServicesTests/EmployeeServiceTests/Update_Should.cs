using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Update;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.EmployeeServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void ReturnUpdatedEmployee()
        {
            var options = Utils.GetOptions(nameof(ReturnUpdatedEmployee));
            var updateEmployeeDTO = new Mock<UpdateEmployeeDTO>().Object;
            updateEmployeeDTO.FirstName = "John";
            updateEmployeeDTO.LastName = "Smith";
            updateEmployeeDTO.Email = "john.smith@gmail.com";
            updateEmployeeDTO.AddressId = 1;
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
                var result = sut.Update(1, updateEmployeeDTO);
                var employee = actContext.Employees.FirstOrDefault(c => c.Id == 1);

                Assert.AreEqual(employee.FirstName, result.FirstName);
                Assert.AreEqual(employee.LastName, result.LastName);
                Assert.AreEqual(employee.Email, result.Email);
                Assert.AreEqual(employee.Address.StreetName + ", " + employee.Address.City.Name, result.Address);
                Assert.IsInstanceOfType(result, typeof(EmployeeDTO));
            }
        }
        [TestMethod]
        public void Update_Throw_When_InvalidEmployeeId()
        {
            var options = Utils.GetOptions(nameof(Update_Throw_When_InvalidEmployeeId));
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new EmployeeService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(10, new UpdateEmployeeDTO()));
            }
        }
        [TestMethod]
        public void Update_Throw_When_InvalidAddressId()
        {
            var options = Utils.GetOptions(nameof(Update_Throw_When_InvalidAddressId));
            var updateEmployeerDTO = new Mock<UpdateEmployeeDTO>().Object;
            updateEmployeerDTO.FirstName = "John";
            updateEmployeerDTO.LastName = "Smith";
            updateEmployeerDTO.Email = "john.smith@gmail.com";
            updateEmployeerDTO.AddressId = 20;
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new EmployeeService(actContext, sutHelp);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(1, updateEmployeerDTO));
            }
        }
    }
}
