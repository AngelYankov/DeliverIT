using DeliverIt.Data;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.CategoryServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void ReturnNameOfAllCategories()
        {
            var options = Utils.GetOptions(nameof(ReturnNameOfAllCategories));
            var categories = Utils.SeedCategories();

            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Categories.AddRange(categories);
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = sut.GetAll();
                Assert.AreEqual(string.Join(",", categories.Select(c => c.Name)),string.Join(",",result));
            }
        }
    }
}
