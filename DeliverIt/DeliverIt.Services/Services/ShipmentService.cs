using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;

namespace DeliverIt.Services.Services
{
    public class ShipmentService : IShipmentService
    {
        private readonly DeliverItContext dbContext;
        public ShipmentService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ShipmentDTO Create(NewShipmentDTO dto)
        {
            var newShipment = new Shipment();

            var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == dto.WarehouseId);
            if (warehouse == null)
            {
                throw new ArgumentNullException("There is no such warehouse.");
            }

            var status = this.dbContext.Statuses.FirstOrDefault(s => s.Id == dto.StatusId);
            if (status == null)
            {
                throw new ArgumentNullException("There is no such status.");
            }

            this.dbContext.Shipments.Add(newShipment);
            warehouse.Shipments.Add(newShipment);
            status.Shipments.Add(newShipment);
            newShipment.CreatedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();

            var createdShipment = FindShipment(newShipment.Id);

            return new ShipmentDTO(createdShipment);
        }
        public IEnumerable<ShipmentDTO> GetAll()
        {
            return this.dbContext.Shipments
                                 .Include(s => s.Status)
                                 .Include(s => s.Warehouse)
                                      .ThenInclude(w => w.Address)
                                 .Where(s => s.IsDeleted == false)
                                 .Select(s => new ShipmentDTO(s));
        }

        public ShipmentDTO Get(int id)
        {
            var shipment = FindShipment(id);
            ShipmentDTO shipmentDTO = new ShipmentDTO(shipment);

            return shipmentDTO;
        }

        public ShipmentDTO Update(int id, UpdateShipmentDTO model)
        {
            var shipment = FindShipment(id);
            if (model.StatusId != 0)
            {
                var status = this.dbContext.Statuses.FirstOrDefault(s => s.Id == model.StatusId);
                if (status == null)
                {
                    throw new ArgumentNullException("There is no such status.");
                }
                shipment.StatusId = model.StatusId;
            }
            if (model.WarehouseId != 0)
            {
                var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == model.WarehouseId);
                if (warehouse == null)
                {
                    throw new ArgumentNullException("There is no such warehouse.");
                }
                shipment.WarehouseId = model.WarehouseId;
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
            this.dbContext.SaveChanges();

            return new ShipmentDTO(shipment);
        }

        public bool Delete(int id)
        {
            var shipment = FindShipment(id);
            shipment.IsDeleted = true;
            shipment.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();

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
                if ((shipment.WarehouseId == warehouseId) && shipment.IsDeleted == false)
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
