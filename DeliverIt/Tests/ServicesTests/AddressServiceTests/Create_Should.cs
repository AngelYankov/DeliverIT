using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ServicesTests.AddressServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnCreatedCustomer()
        {
            var options = Utils.GetOptions(nameof(ReturnCreatedCustomer));
            var newAddressDTO = new NewAddressDTO()
            {
                StreetName = "Ivan Vazov",
                CityId = 1,
            };
            var address = new Address()
            {
                StreetName = "Ivan Vazov",
                CityID = 1,
                CreatedOn = DateTime.UtcNow
            };
            var addressDTO = new AddressDTO(address);
            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Addresses.Add(address);
            }
            //todo
        }
    }
}
