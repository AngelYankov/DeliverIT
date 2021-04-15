using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using Microsoft.EntityFrameworkCore;
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
        public WarehouseDTO Create(NewWarehouseDTO model)
        {
            var warehouse = new Warehouse();
            var adddress = FindAddress(model.AddressId);
            warehouse.AddressId = model.AddressId;
            warehouse.Address = adddress;
            warehouse.CreatedOn = DateTime.UtcNow;
            this.dbContext.Warehouses.Add(warehouse);
            this.dbContext.SaveChanges();
            return new WarehouseDTO(warehouse);
        }
        public WarehouseDTO Get(int id)
        {
            var warehouse = FindWarehouse(id);
            return new WarehouseDTO(warehouse);
        }
        public IEnumerable<WarehouseDTO> GetAll()
        {
            return this.dbContext.Warehouses
                                 .Include(w => w.Address)
                                    .ThenInclude(a => a.City)
                                 .Where(w => w.IsDeleted == false)
                                 .Select(w => new WarehouseDTO(w));
        }
        public WarehouseDTO Update(int id, NewWarehouseDTO model)
        {
            var warehouse = FindWarehouse(id);
            
            if (model.AddressId == 0)
            {
                throw new ArgumentException("Invalid address ID.");
            }
            var address = FindAddress(model.AddressId);
            address.Warehouse = null;
            warehouse.AddressId = address.Id;
            warehouse.ModifiedOn = DateTime.UtcNow;
            //TODO
            this.dbContext.SaveChanges();
            return new WarehouseDTO(warehouse);
        }
        public bool Delete(int id)
        {
            var warehouse = FindWarehouse(id);
            warehouse.IsDeleted = true;
            warehouse.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return true;
        }
        private Address FindAddress(int id)
        {
            return this.dbContext.Addresses
                                .Include(a => a.City)
                                .FirstOrDefault(a => a.Id == id)
                                ?? throw new ArgumentException("There is no such address.");
        }
        private Warehouse FindWarehouse(int id)
        {
            var warehouse = this.dbContext.Warehouses
                                          .Include(w => w.Address)
                                              .ThenInclude(a => a.City)
                                          .FirstOrDefault(w => w.Id == id)
                                          ?? throw new ArgumentException("There is no such warehouse.");
            if (warehouse.IsDeleted)
            {
                throw new ArgumentException("Warehouse has already been deleted.");
            };
            return warehouse;
        }
    }
}
