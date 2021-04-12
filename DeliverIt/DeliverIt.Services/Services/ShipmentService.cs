using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly DeliverItContext dbContext;

        public ShipmentService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //we should add it to a list of shipments for a specific warehouse?
        public Shipment Create(Shipment shipment, int warehouseId)
        {
            var warehouse = dbContext.Warehouses.FirstOrDefault(w => w.Id == warehouseId);
            if(warehouse == null)
            {
                throw new ArgumentNullException();
            }
            dbContext.Shipments.Add(shipment);
            shipment.CreatedOn = DateTime.UtcNow;
            return shipment;
        }
        public IEnumerable<Shipment> GetAll()
        {
            return dbContext.Shipments;
        }
        public Shipment Get(int id)
        {
            var shipment = dbContext.Shipments.FirstOrDefault(s => s.Id == id);
            if (shipment == null)
            {
                throw new ArgumentNullException();
            }
            return shipment;
        }
        //we should update it from a list of shipments for a specific warehouse
        public Shipment Update(int id, Shipment model)
        {
            var shipment = dbContext.Shipments.FirstOrDefault(s => s.Id == id);
            if (shipment == null)
            {
                throw new ArgumentNullException();
            }
            shipment.StatusId = model.StatusId;
            shipment.Arrival = model.Arrival;
            shipment.Departure = model.Departure;
            shipment.ModifiedOn = DateTime.UtcNow;
            return shipment;
        }
        //we should delete it from a list of shipments for a specific warehouse
        public bool Delete(int id)
        {
            var shipment = dbContext.Shipments.FirstOrDefault(s => s.Id == id);
            if (shipment == null)
            {
                throw new ArgumentNullException();
            }
            dbContext.Shipments.Remove(shipment);
            shipment.IsDeleted = true;
            shipment.DeletedOn = DateTime.UtcNow;
            return true;
        }

        public IEnumerable<Shipment> GetBy(string filter, string type)
        {
            throw new NotImplementedException();
        }
    }
}
