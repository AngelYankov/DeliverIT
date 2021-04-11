using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Warehouse : Entity
    {
        [Key]
        public int Id { get; set; }
        public int AddressID { get; set; }
        public  Address Address { get; set; }
        public ICollection<Parcel> Parcels { get; set; } = new HashSet<Parcel>();
        public ICollection<ShipmentWarehouse> ShipmentWarehouses { get; set; } = new HashSet<ShipmentWarehouse>();
    }
}
