using DeliverIt.Data;
using DeliverIt.Services.Contracts;
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
    public class GetEmployee_Should
    {
        [TestMethod]
        public void ReturnEmployeeToVerify()
        {
            var options = Utils.GetOptions(nameof(ReturnEmployeeToVerify));
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Employees.AddRange(Utils.SeedEmployees());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new EmployeeService(actContext, mock.Object);
                var result = sut.GetEmployee("petar.shapkov");
                var employee = actContext.Employees.First();
                Assert.AreEqual(employee, result);
            }
        }
        [TestMethod]
        public void Throw_When_EmployeeIsNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_EmployeeIsNotFound));
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new EmployeeService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.GetEmployee("123"));
            }
        }
    }
}
