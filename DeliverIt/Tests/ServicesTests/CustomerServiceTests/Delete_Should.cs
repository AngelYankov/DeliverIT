using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.CustomerServiceTests
{
    [TestClass]
    public class Delete_Should
    {
        [TestMethod]
        public void ReturnTrueIfDeleted()
        {
            var options = Utils.GetOptions(nameof(ReturnTrueIfDeleted));
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
                var result = sut.Delete(1);
                Assert.AreEqual(actContext.Customers.Where(c => c.IsDeleted == false).Count(), 2);
                Assert.IsTrue(result);
            }
        }
        [TestMethod]
        public void Delete_Throw_When_InvalidCustomerId()
        {
            var options = Utils.GetOptions(nameof(Delete_Throw_When_InvalidCustomerId));
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
               Assert.ThrowsException<ArgumentException>(()=>sut.Delete(10));
            }
        }
    }
}
