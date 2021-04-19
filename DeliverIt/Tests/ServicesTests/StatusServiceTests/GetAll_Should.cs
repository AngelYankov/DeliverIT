using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.StatusServiceTests
{
    [TestClass]
    public class GetAll_Should
    {
        [TestMethod]
        public void Return_All_Statuses()
        {
            var options = Utils.GetOptions(nameof(Return_All_Statuses));
            var statuses = Utils.SeedStatuses();

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Statuses.AddRange(statuses);
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new StatusService(actContext);
                var result = sut.GetAll();

                Assert.AreEqual(statuses.Count, result.Count);
                Assert.AreEqual(string.Join(",", statuses.Select(s => s.Name)), string.Join(",", result));
            }
        }
    }
}
