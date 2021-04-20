using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services
{
    public static class Exceptions
    {
        public const string InvalidCity = "There is no such city.";
        public const string InvalidAddress = "There is no such address.";
        public const string InvalidCategory = "There is no such category.";
        public const string InvalidCountry = "There in no such country.";
        public const string InvalidCustomer = "There is no such customer.";
        public const string InvalidEmployee = "There is no such employee.";
        public const string InvalidParcel = "There is no such parcel.";
        public const string InvalidParcels = "There are no such parcels.";
        public const string InvalidWarehouse = "There is no such warehouse.";
        public const string InvalidStatus = "There is no such status.";
        public const string InvalidShipment = "There is no such shipment.";
        public const string InvalidShipments = "There are no such shipments.";
        public const string DeletedCategory = "Category has already been deleted.";
        public const string DeletedCustomer = "Customer has already been deleted.";
        public const string DeletedEmployee = "Employee has already been deleted.";
        public const string DeletedParcel = "Parcel has already been deleted.";
        public const string DeletedShipment = "Shipment has already been deleted.";
        public const string DeletedWarehouse = "Warehouse has already been deleted.";
        public const string TakenAddress = "Address is already taken.";
        public const string InvalidUsername = "Invalid username.";
        public const string NoCustomers = "There are no customers.";
        public const string NoShipments = "There are no shipments.";
        public const string InvalidWeight = "The weight should be between 0.1 and 500 kg.";

    }
}
