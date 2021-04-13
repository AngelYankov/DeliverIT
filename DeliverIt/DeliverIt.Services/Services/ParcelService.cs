using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DeliverIt.Services.Services
{
    public class ParcelService : IParcelService
    {
        private readonly DeliverItContext dbContext;

        public ParcelService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Parcel Create(Parcel parcel)
        {
            parcel.Customer = this.dbContext.Customers
                                            .Include(c=>c.Address)
                                            .Include(c=>c.Parcels)
                                            .FirstOrDefault(c => c.Id == parcel.CustomerId);
            parcel.Warehouse = this.dbContext.Warehouses
                                             .Include(w => w.Address)
                                                .ThenInclude(a=>a.City)
                                             .Include(w => w.Parcels)
                                             .Include(w => w.Shipments)
                                             .FirstOrDefault(w => w.Id == parcel.WarehouseId);
            dbContext.Parcels.Add(parcel);
            parcel.CreatedOn = DateTime.UtcNow;
            return parcel;
        }

        public List<ParcelDTO> GetAll()
        {
            var allParcels = this.dbContext
                             .Parcels
                             .Include(p => p.Category)
                             .Include(p => p.Customer)
                             .Include(p => p.Warehouse)
                                .ThenInclude(w => w.Address)
                                .ThenInclude(a => a.City);
            var parcels = new List<ParcelDTO>();
            foreach (var parcel in allParcels)
            {
                var parcelDTO = new ParcelDTO(parcel);
                parcels.Add(parcelDTO);
            }
            return parcels;
        }

        public ParcelDTO Get(int id)
        {
            var parcel = this.dbContext
                             .Parcels
                             .Include(p => p.Category)
                             .Include(p => p.Customer)
                             .Include(p => p.Warehouse)
                                .ThenInclude(w => w.Address)
                                .ThenInclude(a => a.City)
                                .FirstOrDefault(c => c.Id == id);
            if (parcel == null)
            {
                throw new ArgumentNullException();
            }

            ParcelDTO parcelDTO = new ParcelDTO(parcel);

            return parcelDTO;
        }

        public Parcel Update(int id, Parcel model)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(c => c.Id == id);
            if (parcel == null)
            {
                throw new ArgumentNullException();
            }

            parcel.Category = model.Category ?? parcel.Category;

            if (model.CustomerId != 0)
            {
                var customer = this.dbContext.Customers.FirstOrDefault(s => s.Id == model.CustomerId);
                if (customer == null)
                {
                    throw new ArgumentNullException();
                }
                parcel.CustomerId = model.CustomerId;
            }
            if (model.ShipmentId != 0)
            {
                var shipment = this.dbContext.Shipments.FirstOrDefault(s => s.Id == model.ShipmentId);
                if (shipment == null)
                {
                    throw new ArgumentNullException();
                }
                parcel.ShipmentId = model.ShipmentId;
            }
            if (model.WarehouseId != 0)
            {
                var warehouse = this.dbContext.Warehouses.FirstOrDefault(s => s.Id == model.WarehouseId);
                if (warehouse == null)
                {
                    throw new ArgumentNullException();
                }
                parcel.WarehouseId = model.WarehouseId;
            }
            if (model.Weight != 0)
            {
                parcel.Weight = model.Weight;
            }
            parcel.ModifiedOn = DateTime.UtcNow;
            return parcel;
        }

        public bool Delete(int id)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(c => c.Id == id);

            if (parcel == null)
            {
                throw new ArgumentNullException();
            }
            dbContext.Parcels.Remove(parcel);
            parcel.Customer.Parcels.Remove(parcel);
            parcel.IsDeleted = true;
            parcel.DeletedOn = DateTime.UtcNow;

            return parcel.IsDeleted;
        }
        public List<ParcelDTO> GetBy(string filter, string value)
        {
            var allParcels = this.dbContext
                            .Parcels
                            .Include(p => p.Category)
                            .Include(p => p.Customer)
                            .Include(p => p.Warehouse)
                               .ThenInclude(w => w.Address)
                               .ThenInclude(a => a.City);
            var parcels = new List<ParcelDTO>();
            if (filter == "weight")
            {
                foreach (var parcel in allParcels)
                {
                    if (parcel.Weight == double.Parse(value))
                    {
                        var parcelDTO = new ParcelDTO(parcel);
                        parcels.Add(parcelDTO);
                    }
                }
            }
            if (filter == "customer")
            {
                foreach (var parcel in allParcels)
                {
                    if (parcel.Customer.FirstName == value || parcel.Customer.LastName == value)
                    {
                        var parcelDTO = new ParcelDTO(parcel);
                        parcels.Add(parcelDTO);
                    }
                }
            }
            if (filter == "warehouse")
            {
                foreach (var parcel in allParcels)
                {
                    if (parcel.WarehouseId == int.Parse(value))
                    {
                        var parcelDTO = new ParcelDTO(parcel);
                        parcels.Add(parcelDTO);
                    }
                }
            }
            if (filter == "category")
            {
                foreach (var parcel in allParcels)
                {
                    if (parcel.CategoryId == int.Parse(value))
                    {
                        var parcelDTO = new ParcelDTO(parcel);
                        parcels.Add(parcelDTO);
                    }
                }
            }
            if(parcels.Count == 0)
            {
                throw new ArgumentNullException();
            }
            
            return parcels;
        }
    }
}
