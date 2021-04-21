using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.CountryServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void Return_Correct_Country()
        {
            var options = Utils.GetOptions(nameof(Return_Correct_Country));
            var countries = Utils.SeedCountries();
            var countryDTO = new CountryDTO(countries.First());

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Countries.AddRange(countries);
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CountryService(actContext);
                var result = sut.Get(1);

                Assert.AreEqual(countryDTO.Id, result.Id);
                Assert.AreEqual(countryDTO.Name, result.Name);
            }
        }

        [TestMethod]
        public void Throw_When_CountryNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_CountryNotFound));

            using (var context = new DeliverItContext(options))
            {
                var sut = new CountryService(context);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Get(1));
            }
        }
    }
}
