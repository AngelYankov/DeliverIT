using DeliverIt.Data;
using DeliverIt.Services.Models;
using DeliverIt.Services.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tests.ServicesTests.ParcelServiceTests
{
    [TestClass]
    public class GetBy_Should
    {
        [TestMethod]
        public void Return_Parcels_Category_Customer_SortBy_WeightAndArrival()
        {
            var options = Utils.GetOptions(nameof(Return_Parcels_Category_Customer_SortBy_WeightAndArrival));

            string filter1 = "category";
            string value1 = "electronics";
            string filter2 = "customer";
            string value2 = "stefan";
            string sortBy1 = "weight";
            string sortBy2 = "arrival";
            string order = "asc";

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var filtered = actContext.Parcels.Where(p => (p.Category.Name.Equals(value1, StringComparison.OrdinalIgnoreCase))
                                && (p.Customer.FirstName.Equals(value2, StringComparison.OrdinalIgnoreCase)
                                || p.Customer.LastName.Equals(value2, StringComparison.OrdinalIgnoreCase)) && p.IsDeleted == false)
                                                      .OrderBy(p => p.Weight).ThenBy(p => p.Shipment.Arrival);

                var result = sut.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, order);

                Assert.AreEqual(string.Join(",", filtered.Select(f => new ParcelDTO(f))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Return_Parcels_Clothing()
        {
            var options = Utils.GetOptions(nameof(Return_Parcels_Clothing));

            string filter1 = "category";
            string value1 = "clothing";
            string filter2 = null;
            string value2 = null;
            string sortBy1 = null;
            string sortBy2 = null;
            string order = null;

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var filtered = actContext.Parcels.Where(p => p.Category.Name.Equals(value1, StringComparison.OrdinalIgnoreCase) && p.IsDeleted == false);
                var result = sut.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, order);

                Assert.AreEqual(string.Join(",", filtered.Select(f => new ParcelDTO(f))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Return_Parcels_SortedBy_Arrival()
        {
            var options = Utils.GetOptions(nameof(Return_Parcels_SortedBy_Arrival));

            string filter1 = null;
            string value1 = null;
            string filter2 = null;
            string value2 = null;
            string sortBy1 = "arrival";
            string sortBy2 = null;
            string order = "asc";

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var filtered = actContext.Parcels.Where(p => p.IsDeleted == false).OrderBy(p => p.Shipment.Arrival);
                var result = sut.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, order);

                Assert.AreEqual(string.Join(",", filtered.Select(f => new ParcelDTO(f))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Return_Parcels_SortedBy_ArrivalAndWeight()
        {
            var options = Utils.GetOptions(nameof(Return_Parcels_SortedBy_ArrivalAndWeight));

            string filter1 = null;
            string value1 = null;
            string filter2 = null;
            string value2 = null;
            string sortBy1 = "arrival";
            string sortBy2 = "weight";
            string order = "asc";

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var filtered = actContext.Parcels.Where(p => p.IsDeleted == false).OrderBy(p => p.Shipment.Arrival).ThenBy(p => p.Weight);
                var result = sut.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, order);

                Assert.AreEqual(string.Join(",", filtered.Select(f => new ParcelDTO(f))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Return_Parcels_Customer()
        {
            var options = Utils.GetOptions(nameof(Return_Parcels_Customer));

            string filter1 = "customer";
            string value1 = "stefan";
            string filter2 = null;
            string value2 = null;
            string sortBy1 = null;
            string sortBy2 = null;
            string order = null;

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);
                var filtered = actContext.Parcels.Where(p => p.Customer.FirstName.Equals(value1, StringComparison.OrdinalIgnoreCase)
                                                          || p.Customer.LastName.Equals(value1, StringComparison.OrdinalIgnoreCase)
                                                          && p.IsDeleted==false);
                var result = sut.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, order);

                Assert.AreEqual(string.Join(",", filtered.Select(f => new ParcelDTO(f))), string.Join(",", result));
            }
        }

        [TestMethod]
        public void Throws_When_Invalid_Filter()
        {
            var options = Utils.GetOptions(nameof(Throws_When_Invalid_Filter));

            string filter1 = "test";
            string value1 = "clothing";
            string filter2 = null;
            string value2 = null;
            string sortBy1 = null;
            string sortBy2 = null;
            string order = null;

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, order));
            }
        }

        [TestMethod]
        public void Throws_When_Invalid_FilterValue()
        {
            var options = Utils.GetOptions(nameof(Throws_When_Invalid_FilterValue));

            string filter1 = "category";
            string value1 = "test";
            string filter2 = null;
            string value2 = null;
            string sortBy1 = null;
            string sortBy2 = null;
            string order = null;

            using (var arrangeContext = new DeliverItContext(options))
            {
                arrangeContext.Parcels.AddRange(Utils.SeedParcels());
                arrangeContext.Customers.AddRange(Utils.SeedCustomers());
                arrangeContext.Warehouses.AddRange(Utils.SeedWarehouses());
                arrangeContext.Categories.AddRange(Utils.SeedCategories());
                arrangeContext.Shipments.AddRange(Utils.SeedShipments());
                arrangeContext.Addresses.AddRange(Utils.SeedAddresses());
                arrangeContext.Cities.AddRange(Utils.SeedCities());
                arrangeContext.SaveChanges();
            }

            using (var actContext = new DeliverItContext(options))
            {
                var sut = new ParcelService(actContext);

                Assert.ThrowsException<ArgumentNullException>(() => sut.GetBy(filter1, value1, filter2, value2, sortBy1, sortBy2, order));
            }
        }
    }
}
