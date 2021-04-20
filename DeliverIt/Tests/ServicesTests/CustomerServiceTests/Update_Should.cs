using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Update;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.CustomerServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void ReturnUpdatedCustomer()
        {
            var options = Utils.GetOptions(nameof(ReturnUpdatedCustomer));
            var updateCustomerDTO = new UpdateCustomerDTO()
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@gmail.com",
                AddressId = 1
            };
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Customers.AddRange(Utils.SeedCustomers());
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new CustomerService(actContext, sutHelp);
                var result = sut.Update(1, updateCustomerDTO);
                var customer = actContext.Customers.FirstOrDefault(c => c.Id == 1);

                Assert.AreEqual(customer.FirstName, result.FirstName);
                Assert.AreEqual(customer.LastName, result.LastName);
                Assert.AreEqual(customer.Email, result.Email);
                Assert.AreEqual(customer.Address.StreetName + ", " + customer.Address.City.Name, result.Address);
                Assert.IsInstanceOfType(result, typeof(CustomerDTO));
            }
        }
        [TestMethod]
        public void Update_Throw_When_InvalidCustomerId()
        {
            var options = Utils.GetOptions(nameof(Update_Throw_When_InvalidCustomerId));
            using (var actContext = new DeliverItContext(options))
            {
                var mock = new Mock<IAddressService>();
                var sut = new CustomerService(actContext, mock.Object);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(10, new UpdateCustomerDTO()));
            }
        }
        [TestMethod]
        public void Update_Throw_When_InvalidAddressId()
        {
            var options = Utils.GetOptions(nameof(Update_Throw_When_InvalidAddressId));
            var updateCustomerDTO = new UpdateCustomerDTO()
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "john.smith@gmail.com",
                AddressId = 20
            };
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new CustomerService(actContext, sutHelp);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(1, updateCustomerDTO));
            }
        }
    }
}
