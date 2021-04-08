using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IShipmentService
    {
        Shipment Get(int id);
        IEnumerable<Shipment> GetAll();
        Shipment Create(Shipment shipment);
        Shipment Update(int id, Shipment shipment);
        bool Delete(int id);
        IEnumerable<Shipment> GetBy(string filter, string type);
    }
}
