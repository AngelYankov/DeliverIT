using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.EntityFrameworkCore;
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
        public void Return_Correct_City()
        {
            var options = Utils.GetOptions(nameof(Return_Correct_City));
            var cities = Utils.SeedCities();
            var countries = Utils.SeedCountries();

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Cities.AddRange(cities);
                arrangeContext.Countries.AddRange(countries);
                arrangeContext.SaveChanges();
            }
            var cityDTO = new CityDTO(cities.First());

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CityService(actContext);
                var result = sut.Get(1);

                Assert.AreEqual(cityDTO.Id, result.Id);
                Assert.AreEqual(cityDTO.City, result.City);
                Assert.AreEqual(cityDTO.Country, result.Country);
            }
        }

        [TestMethod]
        public void Throws_When_CityNotFound()
        {
            var options = Utils.GetOptions(nameof(Throws_When_CityNotFound));
            using (var context = new DeliverItContext(options))
            {
                var sut = new CityService(context);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Get(1));
            }
        }
    }
}
