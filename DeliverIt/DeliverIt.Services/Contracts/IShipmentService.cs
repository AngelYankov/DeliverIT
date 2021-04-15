using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IShipmentService
    {
        ShipmentDTO Get(int id);
        IEnumerable<ShipmentDTO> GetAll();
        ShipmentDTO Create(NewShipmentDTO shipment);
        ShipmentDTO Update(int id, UpdateShipmentDTO shipment);
        bool Delete(int id);
        List<ShipmentDTO> GetBy(string filter, string value);
    }
}
