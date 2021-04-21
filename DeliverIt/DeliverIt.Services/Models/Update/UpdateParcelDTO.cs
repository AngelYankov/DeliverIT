using System;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Services.Models.Update
{
    public class UpdateParcelDTO
    {
        public int CustomerId { get; set; }
        public int WarehouseId { get; set; }
        public int CategoryId { get; set; }
        public int ShipmentId { get; set; }
        [Range(0, 500, ErrorMessage = "Value for {0} should be max {2} kg.")]
        public double Weight { get; set; }
    }
}
