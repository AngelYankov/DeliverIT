using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Warehouse : Entity
    {
        [Key]
        public int Id { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Parcel> Parcels { get; set; } = new HashSet<Parcel>();
        public ICollection<Shipment> Shipments { get; set; } = new HashSet<Shipment>();
    }
}
