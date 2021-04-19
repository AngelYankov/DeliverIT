using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.CityServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Return_All_Cities()
        {
            var options = Utils.GetOptions(nameof(Return_All_Cities));
            var cities = Utils.SeedCities();

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Cities.AddRange(cities);
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CityService(actContext);
                var result = sut.GetAll();

                Assert.AreEqual(cities.Count, result.Count);
                Assert.AreEqual(string.Join(",", cities.Select(c => c.Name)), string.Join(",", result));

            }
        }
    }
}
