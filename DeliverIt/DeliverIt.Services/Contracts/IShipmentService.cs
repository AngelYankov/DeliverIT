using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IShipmentService
    {
        ShipmentDTO Get(int id);
        List<ShipmentDTO> GetAll();
        Shipment Create(Shipment shipment, int warehouseId);
        Shipment Update(int id, Shipment shipment);
        bool Delete(int id);
        IEnumerable<Shipment> GetBy(string filter, string type);
    }
}
