using System.Collections.Generic;

namespace DeliverIt.Data.Models
{
    public class Status : IStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HashSet<Shipment> Shipments { get; set; }
    }
}