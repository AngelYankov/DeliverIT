using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Shipment : IShipment
    {
        public int Id { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public HashSet<Parcel> Parcels { get; set; }
    }
}
