using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DeliverIt.Services.Models;

namespace DeliverIt.Services.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly DeliverItContext dbContext;
        public ShipmentService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Shipment Create(Shipment shipment, int warehouseId)
        {
            var warehouse = dbContext.Warehouses.FirstOrDefault(w => w.Id == warehouseId);
            if (warehouse == null)
            {
                throw new ArgumentNullException();
            }
            dbContext.Shipments.Add(shipment);
            warehouse.Shipments.Add(shipment);
            shipment.CreatedOn = DateTime.UtcNow;
            return shipment;
        }
        public List<ShipmentDTO> GetAll()
        {
            var allShipments = this.dbContext
                               .Shipments
                               .Include(s => s.Status)
                               .Include(s => s.Warehouse)
                                    .ThenInclude(w => w.Address);
            var shipments = new List<ShipmentDTO>();
            foreach (var shipment in allShipments)
            {
                var shipmentDTO = new ShipmentDTO(shipment);
                shipments.Add(shipmentDTO);
            }
            return shipments;
        }
        public ShipmentDTO Get(int id)
        {
            var shipment = FindShipment(id);
            ShipmentDTO shipmentDTO = new ShipmentDTO(shipment);

            return shipmentDTO;
        }

        public Shipment Update(int id, Shipment model)
        {
            var shipment = FindShipment(id);
            if (model.StatusId != 0)
            {
                var status = this.dbContext.Statuses.FirstOrDefault(s => s.Id == model.StatusId);
                if (status == null)
                {
                    throw new ArgumentNullException();
                }
                shipment.StatusId = model.StatusId;
            }
            if (model.Arrival != null)
            {
                shipment.Arrival = model.Arrival;
            }
            if (model.Departure != null)
            {
                shipment.Departure = model.Departure;
            }
            shipment.ModifiedOn = DateTime.UtcNow;
            return shipment;
        }

        public bool Delete(int id)
        {
            var shipment = FindShipment(id);
            shipment.IsDeleted = true;
            shipment.DeletedOn = DateTime.UtcNow;
            return true;
        }

        //api/shipments/search?warehouseId=
        //public IActionResult Get([From Query] int warehouseId)
        public List<ShipmentDTO> GetBy(int warehouseId)
        {
            var allShipments = this.dbContext
                              .Shipments
                              .Include(s => s.Status)
                              .Include(s => s.Warehouse)
                                   .ThenInclude(w => w.Address);
            var shipments = new List<ShipmentDTO>();
            foreach (var shipment in allShipments)
            {
                if (shipment.WarehouseId == warehouseId)
                {
                    var shipmentDTO = new ShipmentDTO(shipment);
                    shipments.Add(shipmentDTO);
                }
            }
            return shipments;
        }

        private Shipment FindShipment(int id)
        {
            var shipment = this.dbContext
                               .Shipments
                               .Include(s => s.Status)
                               .Include(s => s.Warehouse)
                                    .ThenInclude(w => w.Address)
                               .FirstOrDefault(s => s.Id == id);
            if (shipment == null)
            {
                throw new ArgumentNullException("There is no such shipment.");
            }
            if (shipment.IsDeleted)
            {
                throw new ArgumentNullException("Shipment is deleted.");
            }
            return shipment;
        }
    }
}
