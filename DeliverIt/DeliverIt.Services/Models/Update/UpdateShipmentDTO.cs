using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Models.Update
{
    public class UpdateShipmentDTO
    {
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int StatusId { get; set; }
        public int WarehouseId { get; set; }
    }
}
