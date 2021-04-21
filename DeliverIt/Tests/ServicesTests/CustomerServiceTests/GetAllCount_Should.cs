using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace Tests.ServicesTests.CustomerServiceTests
{
    [TestClass]
    public class GetAllCount_Should
    {
        [TestMethod]
        public void ReturnCount()
        {
            var options = Utils.GetOptions(nameof(ReturnCount));
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Customers.AddRange(Utils.SeedCustomers());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
                var result = sut.GetAllCount();
                Assert.AreEqual(actContext.Customers.Count(), result);
            }
        }
    }
}
