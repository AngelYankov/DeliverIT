using System;

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
