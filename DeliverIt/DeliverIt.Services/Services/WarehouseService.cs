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
            //TO set up address - warehouse
            this.dbContext.Warehouses.Add(warehouse);
            warehouse.CreatedOn = DateTime.UtcNow;
            return warehouse;
        }
        public Warehouse Get(int id)
        {
            var warehouse = FindWarehouse(id);
            return warehouse;
        }
        public IEnumerable<Warehouse> GetAll()
        {
            return this.dbContext.Warehouses.Where(w => w.IsDeleted == false);
        }
        public Warehouse Update(int id, Warehouse model)
        {
            var warehouse = FindWarehouse(id);
            warehouse.ModifiedOn = DateTime.UtcNow;
            warehouse = model;
            return warehouse;
        }
        public bool Delete(int id)
        { 
            var warehouse = FindWarehouse(id);
            warehouse.IsDeleted = true;
            warehouse.ModifiedOn = DateTime.UtcNow;
            return true;
        }
        private Warehouse FindWarehouse(int id)
        {
            var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == id);
            if (warehouse == null)
            {
                throw new ArgumentNullException();
            }
            if (warehouse.IsDeleted)
            {
                throw new ArgumentException();
            };
            return warehouse;
        }
    }
}
