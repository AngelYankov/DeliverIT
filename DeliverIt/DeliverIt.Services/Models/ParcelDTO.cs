using DeliverIt.Data.Models;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace DeliverIt.Services.Models
{
    public class ParcelDTO
    {
        public ParcelDTO(Parcel parcel)
        {
            Id = parcel.Id;
            CustomerFirstName = parcel.Customer.FirstName;
            CustomerLastName = parcel.Customer.LastName;
            WarehouseAddress = parcel.Warehouse.Address.StreetName;
            WarehouseCity = parcel.Warehouse.Address.City.Name;
            ParcelArrival = parcel.Shipment.Arrival.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            Category = parcel.Category.Name;
            Weight = parcel.Weight;
            CustomerId = parcel.CustomerId;
            WarehouseId = parcel.WarehouseId;
            ShipmentId = parcel.ShipmentId;
            CategoryId = parcel.CategoryId;
        }
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        [JsonIgnore]
        public int WarehouseId { get; set; }
        public string WarehouseAddress { get; set; }
        public string WarehouseCity { get; set; }
        [JsonIgnore]
        public int ShipmentId { get; set; }
        public string ParcelArrival { get; set; }
        [JsonIgnore]
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public double Weight { get; set; }
    }
}
