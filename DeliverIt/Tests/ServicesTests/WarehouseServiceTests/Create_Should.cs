using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.ServicesTests.WarehouseServiceTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnCreatedWarehouse()
        {
            var options = Utils.GetOptions(nameof(ReturnCreatedWarehouse));

        }
    }
}
