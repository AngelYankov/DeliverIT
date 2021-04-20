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
    public class Delete_Should
    {
        [TestMethod]
        public void ReturnTrueIfDeletedSuccesfully()
        {
            var options = Utils.GetOptions(nameof(ReturnTrueIfDeletedSuccesfully));
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
                var result = sut.Delete(1);
                Assert.AreEqual(actContext.Employees.Where(c => c.IsDeleted == false).Count(), 1);
                Assert.IsTrue(result);
            }
        }
        [TestMethod]
        public void Delete_Throw_When_InvalidEmployeeId()
        {
            var options = Utils.GetOptions(nameof(Delete_Throw_When_InvalidEmployeeId));
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new EmployeeService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.Delete(10));
            }
        }
    }
}
