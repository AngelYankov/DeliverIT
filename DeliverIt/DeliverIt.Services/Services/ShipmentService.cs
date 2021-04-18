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

        /// <summary>
        /// Create a shipment.
        /// </summary>
        /// <param name="dto">Details of the shipment to be created.</param>
        /// <returns>Returns the created shipment or an appropriate error message.</returns>
        public ShipmentDTO Create(NewShipmentDTO dto)
        {
            var newShipment = new Shipment();

            var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == dto.WarehouseId)
                ?? throw new ArgumentNullException(Exceptions.InvalidWarehouse);

            var status = this.dbContext.Statuses.FirstOrDefault(s => s.Id == dto.StatusId)
                ?? throw new ArgumentNullException(Exceptions.InvalidStatus);

            this.dbContext.Shipments.Add(newShipment);
            warehouse.Shipments.Add(newShipment);
            status.Shipments.Add(newShipment);
            newShipment.CreatedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();

            var createdShipment = FindShipment(newShipment.Id);

            return new ShipmentDTO(createdShipment);
        }

        /// <summary>
        /// Get all shippemnts.
        /// </summary>
        /// <returns>Returns all shipments.</returns>
        public IEnumerable<ShipmentDTO> GetAll()
        {
            return this.dbContext.Shipments
                                 .Include(s => s.Status)
                                 .Include(s => s.Warehouse)
                                      .ThenInclude(w => w.Address)
                                 .Where(s => s.IsDeleted == false)
                                 .Select(s => new ShipmentDTO(s));
        }

        /// <summary>
        /// Get all shipments count
        /// </summary>
        /// <returns>Returns the number of shipments.</returns>
        public int GetAllCount()
        {
            var allShipmentsCount = this.dbContext.Shipments.Where(s=>s.IsDeleted==false && s.Arrival > DateTime.UtcNow).Count();
           
            return allShipmentsCount;
        }

        /// <summary>
        /// Get a shipment by a certain ID.
        /// </summary>
        /// <param name="id">ID of the shipment to get.</param>
        /// <returns>Returns a shipment by a certain ID or an appropriate error message.</returns>
        public ShipmentDTO Get(int id)
        {
            var shipment = FindShipment(id);
            ShipmentDTO shipmentDTO = new ShipmentDTO(shipment);

            return shipmentDTO;
        }

        /// <summary>
        /// Update a shipment.
        /// </summary>
        /// <param name="id">ID of the shipment to be updated.</param>
        /// <param name="model">Details of the shipment to be updated.</param>
        /// <returns>Returns the updated shipment or an appropriate error message.</returns>
        public ShipmentDTO Update(int id, UpdateShipmentDTO model)
        {
            var shipment = FindShipment(id);
            if (model.StatusId != 0)
            {
                var status = this.dbContext.Statuses.FirstOrDefault(s => s.Id == model.StatusId)
                    ?? throw new ArgumentNullException(Exceptions.InvalidStatus);

                shipment.StatusId = model.StatusId;
            }
            if (model.WarehouseId != 0)
            {
                var warehouse = this.dbContext.Warehouses.Include(w=>w.Address)
                                                         .FirstOrDefault(w => w.Id == model.WarehouseId)
                                                         ?? throw new ArgumentNullException(Exceptions.InvalidWarehouse);

                shipment.WarehouseId = model.WarehouseId;
            }
            if (model.Arrival != DateTime.MinValue)
            {
                shipment.Arrival = model.Arrival;
            }
            if (model.Departure != DateTime.MinValue)
            {
                shipment.Departure = model.Departure;
            }
            shipment.ModifiedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();

            return new ShipmentDTO(shipment);
        }

        /// <summary>
        /// Delete a shipment.
        /// </summary>
        /// <param name="id">ID of the shipment to be deleted.</param>
        /// <returns>Returns response code and an appropriate message.</returns>
        public bool Delete(int id)
        {
            var shipment = FindShipment(id);
            shipment.IsDeleted = true;
            shipment.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return shipment.IsDeleted;
        }

        /// <summary>
        /// Filter shipments.
        /// </summary>
        /// <param name="filter">Filters for shipments.</param>
        /// <param name="value">Value of the filter</param>
        /// <returns>Returns the filtered shipments.</returns>
        public List<ShipmentDTO> GetBy(string filter, string value)
        {
            var allShipments = this.dbContext
                              .Shipments
                              .Include(s => s.Status)
                              .Include(s => s.Warehouse)
                                   .ThenInclude(w => w.Address)
                              .Include(s => s.Parcels)
                                   .ThenInclude(p => p.Customer);
            var shipments = new List<ShipmentDTO>();

            if (filter == "warehouse")
            {
                foreach (var shipment in allShipments)
                {
                    if ((shipment.WarehouseId == int.Parse(value)) && shipment.IsDeleted == false)
                    {
                        var shipmentDTO = new ShipmentDTO(shipment);
                        shipments.Add(shipmentDTO);
                    }
                }
            }

            if (filter == "customer")
            {
                foreach (var shipment in allShipments)
                {
                    if ((shipment.Parcels.Select(p => p.Customer)
                        .Where(c => c.FirstName.Equals(value, StringComparison.OrdinalIgnoreCase) || c.LastName.Equals(value, StringComparison.OrdinalIgnoreCase))
                        .Count() > 0) && shipment.IsDeleted == false)
                    {
                        var shipmentDTO = new ShipmentDTO(shipment);
                        shipments.Add(shipmentDTO);
                    }
                }
            }
            if (shipments.Count() == 0)
            {
                throw new ArgumentNullException(Exceptions.InvalidShipments);
            }
            return shipments;
        }

        /// <summary>
        /// Find a shipment by certain ID.
        /// </summary>
        /// <param name="id">ID of the shipment to find.</param>
        /// <returns>Returns a shipment with certain ID or an appropriate error message.</returns>
        private Shipment FindShipment(int id)
        {
            var shipment = this.dbContext
                               .Shipments
                               .Include(s => s.Status)
                               .Include(s => s.Warehouse)
                                    .ThenInclude(w => w.Address)
                               .FirstOrDefault(s => s.Id == id)
                               ?? throw new ArgumentNullException(Exceptions.InvalidShipment);

            if (shipment.IsDeleted)
            {
                throw new ArgumentNullException(Exceptions.DeletedShipment);
            }
            return shipment;
        }
    }
}
