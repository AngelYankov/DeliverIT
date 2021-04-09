using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Parcel : Entity
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }

        [Required, Range(0.1, 500, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public double Weight { get; set; }
    }
}
