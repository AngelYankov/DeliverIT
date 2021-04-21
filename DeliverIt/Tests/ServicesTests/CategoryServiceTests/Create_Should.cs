using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace Tests.ServicesTests.CategoryServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnNameOfCreatedCategory()
        {
            var options = Utils.GetOptions(nameof(ReturnNameOfCreatedCategory));
            var category = new Mock<Category>().Object;
            category.Name = "Test category";
            using (var arrContext = new DeliverItContext(options))
            {
                arrContext.Categories.Add(category);
                arrContext.SaveChanges();
            }
            using (var actContext = new DeliverItContext(options))
            {
                var sut = new CategoryService(actContext);
                var result = sut.Create("Test category");
                Assert.AreEqual(result, category.Name);
                Assert.AreEqual(actContext.Categories.Count(),2);
            }
        }
    }
}
