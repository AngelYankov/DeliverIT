using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ServicesTests.CountryServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Return_All_Countries()
        {
            var options = Utils.GetOptions(nameof(Return_All_Countries));
            var countries = Utils.SeedCountries();

            using (var arrnageContext = new DeliverItContext(options))
            {
                arrnageContext.Countries.AddRange(countries);
                arrnageContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CountryService(actContext);
                var result = sut.GetAll();

                Assert.AreEqual(countries.Count, result.Count);
                Assert.IsInstanceOfType(countries, (Type)result);
            }
        }
    }
}
