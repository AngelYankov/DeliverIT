using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models
{
    public class ShipmentDTO
    {
        public ShipmentDTO(Shipment shipment)
        {
            Departure = shipment.Departure.ToString("dddd, dd MMMM yyyy");
            Arrival = shipment.Arrival.ToString("dddd dd MMMM yyyy");
            Status = shipment.Status.Name;
            Warehouse = shipment.Warehouse.Address.StreetName;
        }
        
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public string Status { get; set; }
        public string Warehouse { get; set; }
    }
}
