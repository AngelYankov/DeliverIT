using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
using System.Collections.Generic;

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
        int GetAllCount();
    }
}
