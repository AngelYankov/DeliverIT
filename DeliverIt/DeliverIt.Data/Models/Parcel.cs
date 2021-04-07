using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Parcel 
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int WarehouseId { get; set; }
        public double Weight { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Warehouse Warehouse { get; set; }
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
    }
}
