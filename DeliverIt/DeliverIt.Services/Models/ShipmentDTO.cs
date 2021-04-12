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
            Departure = shipment.Departure;
            Arrival = shipment.Arrival;
            Status = shipment.Status.Name;
            Warehouse = shipment.Warehouse.Address.StreetName;
        }
        
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public string Status { get; set; }
        public string Warehouse { get; set; }
        public IList<ShipmentDTO> Shipments { get; set; }

    }
}
