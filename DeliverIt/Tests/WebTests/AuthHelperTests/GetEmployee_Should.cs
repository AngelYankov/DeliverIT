using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Web.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.WebTests.AuthHelperTests
{
    [TestClass]
    public class GetEmployee_Should
    {
        [TestMethod]
        public void ReturnCorrectUser()
        {
            //Arrange
            var employee =
                            new Employee()
                            {
                                Id = 1,
                                FirstName = "Test",
                                LastName = "Emlpoyee",
                                Email = "te@gmail.com",
                                AddressId = 1
                            };
            var mockService = new Mock<IEmployeeService>();
            var customerService = new Mock<ICustomerService>();

            mockService.SetupSequence(x => x.GetEmployee(It.IsAny<string>()))
                .Returns(employee);

            var sut = new AuthHelper(customerService.Object,mockService.Object);

            //Act
            var actual = sut.TryGetEmployee("test.employee");

            //Assert
            mockService.Verify(x => x.GetEmployee(It.IsAny<string>()), Times.Once());
            Assert.AreSame(employee, actual);
        }
    }
}
