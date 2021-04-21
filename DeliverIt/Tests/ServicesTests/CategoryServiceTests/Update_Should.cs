using DeliverIt.Data;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Tests.ServicesTests.CategoryServiceTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void ReturnUpdatedCategoryName()
        {
            var options = Utils.GetOptions(nameof(ReturnUpdatedCategoryName));
            var categories = Utils.SeedCategories();

            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Categories.AddRange(categories);
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = sut.Update(1, "Test category");
                Assert.AreEqual(actContext.Categories.First().Name, result);
            }
        }
        [TestMethod]
        public void Throw_When_CategoryNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_CategoryNotFound));
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CategoryService(actContext);
                Assert.ThrowsException<ArgumentException>(() => sut.Update(1, "Test category"));
            }
        }
    }
}
