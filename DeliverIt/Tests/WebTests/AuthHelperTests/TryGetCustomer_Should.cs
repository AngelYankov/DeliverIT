using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.WebTests.AuthHelperTests
{
    [TestClass]
    public class TryGetCustomer_Should
    {
        [TestMethod]
        public void ReturnCorrectUser()
        {
            //Arrange
            var customer = new Customer()
                            {
                                Id = 1,
                                FirstName = "Test",
                                LastName = "Customer",
                                Email = "tc@gmail.com",
                                AddressId = 1
                            };
            var mockService = new Mock<ICustomerService>();
            var employeeService = new Mock<IEmployeeService>();

            mockService.SetupSequence(x => x.GetCustomer(It.IsAny<string>()))
                .Returns(customer);

            var sut = new AuthHelper(mockService.Object, employeeService.Object);

            //Act
            var actual = sut.TryGetCustomer("test.customer");

            //Assert
            mockService.Verify(x => x.GetCustomer(It.IsAny<string>()), Times.Once());
            Assert.AreSame(customer, actual);
        }
    }
}
