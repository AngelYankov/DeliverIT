using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Shipment : Entity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public ICollection<Parcel> Parcels { get; set; } = new HashSet<Parcel>();
    }
}
