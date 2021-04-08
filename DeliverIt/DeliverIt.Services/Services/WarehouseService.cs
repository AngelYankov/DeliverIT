using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class WarehouseService : IWarehouseService
    {
        public Warehouse Create(Warehouse warehouse)
        {
            Database.Warehouses.Add(warehouse);
            warehouse.CreatedOn = DateTime.UtcNow;
            return warehouse;
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Warehouse Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Warehouse> GetAll()
        {
            throw new NotImplementedException();
        }

        public Warehouse Update(int id, Address address)
        {
            throw new NotImplementedException();
        }
    }
}
