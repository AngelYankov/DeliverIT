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
    public class Delete_Should
    {
        [TestMethod]
        public void ReturnTrueIfCategoryIsDeleted()
        {
            var options = Utils.GetOptions(nameof(ReturnTrueIfCategoryIsDeleted));
            var categories = Utils.SeedCategories();

            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Categories.AddRange(categories);
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = sut.Delete(1);
                Assert.AreEqual(actContext.Categories.Where(c => c.IsDeleted == false).Count(), 2);
                Assert.IsTrue(result);
            }
        }
        [TestMethod]
        public void Throw_When_CategoryIsNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_CategoryIsNotFound));
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CategoryService(actContext);
                Assert.ThrowsException<ArgumentException>(() => sut.Delete(1));
            }
        }
    }
}
