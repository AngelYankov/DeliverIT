using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly DeliverItContext dbContext;

        public WarehouseService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Warehouse Create(Warehouse warehouse)
        {
            dbContext.Warehouses.Add(warehouse);
            warehouse.CreatedOn = DateTime.UtcNow;
            return warehouse;
        }

        public Warehouse Get(int id)
        {
            var warehouse = dbContext.Warehouses.FirstOrDefault(w => w.Id == id);
            if(warehouse == null)
            {
                throw new ArgumentNullException();
            }
            return warehouse;
        }

        public IEnumerable<Warehouse> GetAll()
        {
            return dbContext.Warehouses;
        }

        public Warehouse Update(int id, Address address)
        {
            throw new NotImplementedException();
        }
        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
