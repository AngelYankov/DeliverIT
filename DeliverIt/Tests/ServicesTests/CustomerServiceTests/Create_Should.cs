using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Tests.ServicesTests.CustomerServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnNewCustomer()
        {
            var options = Utils.GetOptions(nameof(ReturnNewCustomer));
            var newCustomerDTO = new Mock<NewCustomerDTO>().Object;
            newCustomerDTO.FirstName = "John";
            newCustomerDTO.LastName = "Smith";
            newCustomerDTO.Email = "john.smith@gmail.com";
            newCustomerDTO.AddressId = 1;
            var customer = new Mock<Customer>().Object;
            customer.FirstName = "John";
            customer.LastName = "Smith";
            customer.Email = "john.smith@gmail.com";
            customer.AddressId = 1;
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Customers.Add(customer);
                arrContext.Addresses.AddRange(Utils.SeedAddresses());
                arrContext.Cities.AddRange(Utils.SeedCities());
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new CustomerService(actContext, sutHelp);
                var result = sut.Create(newCustomerDTO);
                Assert.AreEqual(customer.FirstName, result.FirstName);
                Assert.AreEqual(customer.LastName, result.LastName);
                Assert.AreEqual(customer.Email, result.Email);
                Assert.AreEqual(customer.Address.StreetName + ", " + customer.Address.City.Name, result.Address);
                Assert.AreEqual(actContext.Customers.Count(), 2);
                Assert.IsInstanceOfType(result, typeof(CustomerDTO));
            }
        }
        [TestMethod]
        public void Throw_When_InvalidAddress()
        {
            var options = Utils.GetOptions(nameof(Throw_When_InvalidAddress));
            var newCustomerDTO = new Mock<NewCustomerDTO>().Object;
            newCustomerDTO.FirstName = "John";
            newCustomerDTO.LastName = "Smith";
            newCustomerDTO.Email = "john.smith@gmail.com";
            newCustomerDTO.AddressId = 13;
            using (var actContext = new DeliverItContext(options))
            {
                var sutHelp = new AddressService(actContext);
                var sut = new CustomerService(actContext, sutHelp);
                Assert.ThrowsException<ArgumentException>(() => sut.Create(newCustomerDTO));
            }
        }
    }
}
