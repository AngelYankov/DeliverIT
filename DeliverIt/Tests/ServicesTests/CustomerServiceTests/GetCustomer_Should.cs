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
    public class GetCustomer_Should
    {
        [TestMethod]
        public void ReturnCustomerToVerify()
        {
            var options = Utils.GetOptions(nameof(ReturnCustomerToVerify));
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Customers.AddRange(Utils.SeedCustomers());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
                var result = sut.GetCustomer("stefan.popov");
                var customer = actContext.Customers.First();
                Assert.AreEqual(customer, result);
            }
        }
        [TestMethod]
        public void Throw_When_CustomerIsNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_CustomerIsNotFound));
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.GetCustomer("123"));
            }
        }
    }
}
