﻿using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;

namespace DeliverIt.Services.Services
{
    public class ParcelService : IParcelService
    {
        private readonly DeliverItContext dbContext;

        public ParcelService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public ParcelDTO Create(NewParcelDTO dto)
        {
            var newParcel = new Parcel();

            var category = this.dbContext.Categories.FirstOrDefault(c => c.Id == dto.CategoryId);
            if (category == null)
            {
                throw new ArgumentNullException("There is no such category.");
            }
            newParcel.CategoryId = dto.CategoryId;

            var customer = this.dbContext.Customers.FirstOrDefault(c => c.Id == dto.CustomerId);
            if (customer == null)
            {
                throw new ArgumentNullException("There is no such customer.");
            }
            newParcel.CustomerId = dto.CustomerId;

            var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == dto.WarehouseId);
            if (warehouse == null)
            {
                throw new ArgumentNullException("There is no such warehouse.");
            }
            newParcel.WarehouseId = dto.WarehouseId;

            var shipment = this.dbContext.Shipments.FirstOrDefault(s => s.Id == dto.ShipmentId);
            if (shipment == null)
            {
                throw new ArgumentNullException("There is no such shipment.");
            }
            newParcel.ShipmentId = dto.ShipmentId;

            newParcel.Weight = dto.Weight;
            newParcel.CreatedOn = DateTime.UtcNow;

            this.dbContext.Parcels.Add(newParcel);
            warehouse.Parcels.Add(newParcel);
            category.Parcels.Add(newParcel);
            shipment.Parcels.Add(newParcel);
            customer.Parcels.Add(newParcel);

            this.dbContext.SaveChanges();

            var createdParcel = FindParcel(newParcel.Id);

            ParcelDTO parcelDTO = new ParcelDTO(createdParcel);
            return parcelDTO;
        }

        public IEnumerable<ParcelDTO> GetAll()
        {
            return this.dbContext.Parcels
                                 .Include(p => p.Category)
                                 .Include(p => p.Customer)
                                 .Include(p => p.Shipment)
                                 .Include(p => p.Warehouse)
                                    .ThenInclude(w => w.Address)
                                        .ThenInclude(a => a.City)
                                 .Where(p => p.IsDeleted == false)
                                 .Select(p => new ParcelDTO(p));
        }

        public ParcelDTO Get(int id)
        {
            var parcel = FindParcel(id);
            ParcelDTO parcelDTO = new ParcelDTO(parcel);

            return parcelDTO;
        }

        public ParcelDTO Update(int id, UpdateParcelDTO model)
        {
            var parcel = FindParcel(id);
            var result = this.dbContext.Parcels
                          .Include(p => p.Category)
                          .Include(p => p.Customer)
                          .Include(p => p.Shipment)
                          .Include(p => p.Warehouse)
                             .ThenInclude(w => w.Address)
                                .ThenInclude(a => a.City);
            if (model.CategoryId != 0)
            {
                var category = this.dbContext.Categories.FirstOrDefault(c => c.Id == model.CategoryId);
                if (category == null)
                {
                    throw new ArgumentNullException("There is no such category.");
                }
                parcel.CategoryId = model.CategoryId;
            }
            if (model.CustomerId != 0)
            {
                var customer = this.dbContext.Customers.FirstOrDefault(s => s.Id == model.CustomerId);
                if (customer == null)
                {
                    throw new ArgumentNullException("There is no such customer.");
                }
                parcel.CustomerId = model.CustomerId;
            }
            if (model.ShipmentId != 0)
            {
                var shipment = this.dbContext.Shipments.FirstOrDefault(s => s.Id == model.ShipmentId);
                if (shipment == null)
                {
                    throw new ArgumentNullException("There is no such shipment.");
                }
                parcel.ShipmentId = model.ShipmentId;
            }
            if (model.WarehouseId != 0)
            {
                var warehouse = this.dbContext.Warehouses
                                              .Include(w => w.Address)
                                                .ThenInclude(a => a.City)
                                              .FirstOrDefault(s => s.Id == model.WarehouseId);
                if (warehouse == null)
                {
                    throw new ArgumentNullException("There is no such warehouse.");
                }
                parcel.WarehouseId = model.WarehouseId;
            }
            if (model.Weight != 0)
            {
                parcel.Weight = model.Weight;
            }
            parcel.ModifiedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();

            return new ParcelDTO(parcel);
        }

        public bool Delete(int id)
        {
            var parcel = FindParcel(id);
            parcel.IsDeleted = true;
            parcel.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();

            return parcel.IsDeleted;
        }

        public List<ParcelDTO> GetBy(string filter1, string value1, string filter2, string value2, string sortBy1, string sortBy2, string sortingValue)
        {
            var allParcels = this.dbContext
                            .Parcels
                            .Include(p => p.Category)
                            .Include(p => p.Customer)
                            .Include(p => p.Shipment)
                            .Include(p => p.Warehouse)
                                .ThenInclude(w => w.Address)
                                    .ThenInclude(a => a.City);
            var filteredParcels = new List<ParcelDTO>();
            if (filter1 == "weight")
            {
                switch (filter2)
                {
                    case null:
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Weight == double.Parse(value1)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "customer":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Weight == double.Parse(value1)
                                && (parcel.Customer.FirstName == value2 || parcel.Customer.LastName == value2) && parcel.IsDeleted == false))
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "warehouse":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Weight == double.Parse(value1)
                                && (parcel.WarehouseId == int.Parse(value2)) && parcel.IsDeleted == false))
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "category":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Weight == double.Parse(value1)
                                && (parcel.CategoryId == int.Parse(value2)) && parcel.IsDeleted == false))
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                }
            }
            if (filter1 == "customer")
            {
                switch (filter2)
                {
                    case null:
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Customer.FirstName == value1 || parcel.Customer.LastName == value1) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "weight":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Customer.FirstName == value1 || parcel.Customer.LastName == value1)
                                && (parcel.Weight == double.Parse(value2)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "warehouse":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Customer.FirstName == value1 || parcel.Customer.LastName == value1)
                                && (parcel.WarehouseId == int.Parse(value2)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "category":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Customer.FirstName == value1 || parcel.Customer.LastName == value1)
                                && (parcel.CategoryId == int.Parse(value2)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                }
            }
            if (filter1 == "warehouse")
            {
                switch (filter2)
                {
                    case null:
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.WarehouseId == int.Parse(value1)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "weight":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.WarehouseId == int.Parse(value1))
                                && (parcel.Weight == double.Parse(value2)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "customer":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.WarehouseId == int.Parse(value1))
                                && (parcel.Customer.FirstName == value2 || parcel.Customer.LastName == value2) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "category":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.WarehouseId == int.Parse(value1))
                                && (parcel.CategoryId == int.Parse(value2)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                }
            }
            if (filter1 == "category")
            {
                switch (filter2)
                {
                    case null:
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.CategoryId == int.Parse(value1)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "weight":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.CategoryId == int.Parse(value1))
                                && (parcel.Weight == double.Parse(value2)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "customer":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.CategoryId == int.Parse(value1))
                                && (parcel.Customer.FirstName == value2 || parcel.Customer.LastName == value2) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "warehouse":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.CategoryId == int.Parse(value1))
                                && (parcel.WarehouseId == int.Parse(value2)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                }
            }
            if (filteredParcels.Count == 0 && sortBy1 == null)
            {
                throw new ArgumentNullException("There are no such parcels.");
            }
            if (filteredParcels.Count != 0)
            {
                if (sortBy1 == "weight" && sortBy2 == null)
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = filteredParcels.OrderBy(p => p.Weight).ToList();
                            break;
                        case "desc":
                            filteredParcels = filteredParcels.OrderByDescending(p => p.Weight).ToList();
                            break;
                    }
                }
                if (sortBy1 == "arrival" && sortBy2 == null)
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = filteredParcels.OrderBy(p => p.ParcelArrival).ToList();
                            break;
                        case "desc":
                            filteredParcels = filteredParcels.OrderByDescending(p => p.ParcelArrival).ToList();
                            break;
                    }
                }
                if (sortBy1 == "weight" && sortBy2 == "arrival")
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = filteredParcels.OrderBy(p => p.Weight).ThenBy(p => p.ParcelArrival).ToList();
                            break;
                        case "desc":
                            filteredParcels = filteredParcels.OrderByDescending(p => p.Weight).ThenBy(p => p.ParcelArrival).ToList();
                            break;
                    }
                }
                if (sortBy1 == "arrival" && sortBy2 == "weight")
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = filteredParcels.OrderBy(p => p.ParcelArrival).ThenBy(p => p.Weight).ToList();
                            break;
                        case "desc":
                            filteredParcels = filteredParcels.OrderByDescending(p => p.ParcelArrival).ThenBy(p => p.Weight).ToList();
                            break;
                    }
                }
            }
            else
            {
                var allParcelsDTO = new List<ParcelDTO>();
                foreach (var parcel in allParcels)
                {
                    var parcelDTO = new ParcelDTO(parcel);
                    allParcelsDTO.Add(parcelDTO);
                }
                if (sortBy1 == "weight" && sortBy2 == null)
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = allParcelsDTO.OrderBy(p => p.Weight).ToList();
                            break;
                        case "desc":
                            filteredParcels = allParcelsDTO.OrderByDescending(p => p.Weight).ToList();
                            break;
                    }
                }
                if (sortBy1 == "arrival" && sortBy2 == null)
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = allParcelsDTO.OrderBy(p => p.ParcelArrival).ToList();
                            break;
                        case "desc":
                            filteredParcels = allParcelsDTO.OrderByDescending(p => p.ParcelArrival).ToList();
                            break;
                    }
                }
                if (sortBy1 == "weight" && sortBy2 == "arrival")
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = allParcelsDTO.OrderBy(p => p.Weight).ThenBy(p => p.ParcelArrival).ToList();
                            break;
                        case "desc":
                            filteredParcels = allParcelsDTO.OrderByDescending(p => p.Weight).ThenBy(p => p.ParcelArrival).ToList();
                            break;
                    }
                }
                if (sortBy1 == "arrival" && sortBy2 == "weight")
                {
                    switch (sortingValue)
                    {
                        case "asc":
                            filteredParcels = allParcelsDTO.OrderBy(p => p.ParcelArrival).ThenBy(p => p.Weight).ToList();
                            break;
                        case "desc":
                            filteredParcels = allParcelsDTO.OrderByDescending(p => p.ParcelArrival).ThenBy(p => p.Weight).ToList();
                            break;
                    }
                }
            }

            return filteredParcels;
        }

        private Parcel FindParcel(int id)
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
                throw new ArgumentNullException("There is no such parcel.");
            }
            if (parcel.IsDeleted)
            {
                throw new ArgumentNullException("Parcel is deleted.");
            }
            return parcel;
        }
    }
}
