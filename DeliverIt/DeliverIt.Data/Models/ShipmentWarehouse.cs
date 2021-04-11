using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class ShipmentWarehouse
    {
        public ShipmentWarehouse(int shipmentId, int warehouseId)
        {
            this.ShipmentId = shipmentId;
            this.WarehouseId = warehouseId;
        }
        public int ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
