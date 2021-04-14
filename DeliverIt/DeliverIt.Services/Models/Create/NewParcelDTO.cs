using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models.Create
{
    public class NewParcelDTO
    {
        public int CustomerId { get; set; }
        public int WarehouseId { get; set; }
        public int CategoryId { get; set; }
        public int ShipmentId { get; set; }
        public double Weight { get; set; }
    }
}
