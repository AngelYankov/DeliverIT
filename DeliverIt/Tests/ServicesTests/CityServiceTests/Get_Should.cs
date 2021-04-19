using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.CityServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void ReturnCorrectCity()
        {
            var options = Utils.GetOptions(nameof(ReturnCorrectCity));
            var cities = Utils.SeedCities();
            var cityDTO = new CityDTO(cities.First());

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Cities.AddRange(cities);
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CityService(actContext);
                var result = sut.Get(1);

                Assert.AreEqual(cityDTO.Id, result.Id);
                Assert.AreEqual(cityDTO.City, result.City);
                Assert.AreEqual(cityDTO.Country, result.Country);
            }
        }
    }
}
