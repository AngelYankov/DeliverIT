using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.StatusServiceTests
{
    [TestClass]
    public class Get_Should
    {
        [TestMethod]
        public void Return_Correct_Status()
        {
            var options = Utils.GetOptions(nameof(Return_Correct_Status));
            var statuses = Utils.SeedStatuses();
            var statusDTO = new StatusDTO(statuses.First());

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Statuses.AddRange(statuses);
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new StatusService(actContext);
                var result = sut.Get(1);

                Assert.AreEqual(statusDTO.Id, result.Id);
                Assert.AreEqual(statusDTO.Status, result.Status);
            }
        }

        [TestMethod]
        public void Throw_When_StatusNotFound()
        {
            var options = Utils.GetOptions(nameof(Throw_When_StatusNotFound));
            using (var context = new DeliverItContext(options))
            {
                var sut = new StatusService(context);

                Assert.ThrowsException<ArgumentNullException>(() => sut.Get(1));
            }
        }
    }
}
