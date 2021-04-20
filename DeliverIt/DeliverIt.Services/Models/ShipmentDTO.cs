using DeliverIt.Data.Models;
using Newtonsoft.Json;

namespace DeliverIt.Services.Models
{
    public class ShipmentDTO
    {
        public ShipmentDTO(Shipment shipment)
        {
            Id = shipment.Id;
            Departure = shipment.Departure.ToString("dd.MMMM.yyyy");
            Arrival = shipment.Arrival.ToString("dd.MMMM.yyyy");
            Status = shipment.Status.Name;
            Warehouse = shipment.Warehouse.Address.StreetName;
            StatusId = shipment.StatusId;
            WarehouseId = shipment.WarehouseId;
        }
        [JsonIgnore]
        public int Id { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        [JsonIgnore]
        public int StatusId { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public int WarehouseId { get; set; }
        public string Warehouse { get; set; }
    }
}
