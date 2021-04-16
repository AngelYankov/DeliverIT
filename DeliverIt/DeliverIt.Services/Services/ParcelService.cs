using DeliverIt.Data;
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

        /// <summary>
        /// Create a parcel.
        /// </summary>
        /// <param name="dto">Details of the parcel to be created.</param>
        /// <returns>Returns the created parcel or an appropriate error message.</returns>
        public ParcelDTO Create(NewParcelDTO dto)
        {
            var newParcel = new Parcel();

            var category = this.dbContext.Categories.FirstOrDefault(c => c.Id == dto.CategoryId)
                ?? throw new ArgumentNullException(Exceptions.InvalidCategory);

            newParcel.CategoryId = dto.CategoryId;

            var customer = this.dbContext.Customers.FirstOrDefault(c => c.Id == dto.CustomerId)
                ?? throw new ArgumentNullException(Exceptions.InvalidCustomer);

            newParcel.CustomerId = dto.CustomerId;

            var warehouse = this.dbContext.Warehouses.FirstOrDefault(w => w.Id == dto.WarehouseId)
                ?? throw new ArgumentNullException(Exceptions.InvalidWarehouse);

            newParcel.WarehouseId = dto.WarehouseId;

            var shipment = this.dbContext.Shipments.FirstOrDefault(s => s.Id == dto.ShipmentId)
                ?? throw new ArgumentNullException(Exceptions.InvalidShipment);

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

        /// <summary>
        /// Get all parcels.
        /// </summary>
        /// <returns>Returns all parcels.</returns>
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

        /// <summary>
        /// Get a parcel by a certain ID.
        /// </summary>
        /// <param name="id">ID of the parcel to get.</param>
        /// <returns>Returns a parcel with certain ID or an appropriate error message.</returns>
        public ParcelDTO Get(int id)
        {
            var parcel = FindParcel(id);
            ParcelDTO parcelDTO = new ParcelDTO(parcel);

            return parcelDTO;
        }

        /// <summary>
        /// Update a parcel by a certain ID and data.
        /// </summary>
        /// <param name="id">ID of the parcel to update.</param>
        /// <param name="model">Details of the parcel to be updated.</param>
        /// <returns>Returns the updated parcel or an appropriate error message.</returns>
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
                var category = this.dbContext.Categories.FirstOrDefault(c => c.Id == model.CategoryId)
                    ?? throw new ArgumentNullException(Exceptions.InvalidCategory);

                parcel.CategoryId = model.CategoryId;
            }
            if (model.CustomerId != 0)
            {
                var customer = this.dbContext.Customers.FirstOrDefault(s => s.Id == model.CustomerId)
                    ?? throw new ArgumentNullException(Exceptions.InvalidCustomer);

                parcel.CustomerId = model.CustomerId;
            }
            if (model.ShipmentId != 0)
            {
                var shipment = this.dbContext.Shipments.FirstOrDefault(s => s.Id == model.ShipmentId)
                    ?? throw new ArgumentNullException(Exceptions.InvalidShipment);

                parcel.ShipmentId = model.ShipmentId;
            }
            if (model.WarehouseId != 0)
            {
                var warehouse = this.dbContext.Warehouses
                                              .Include(w => w.Address)
                                                .ThenInclude(a => a.City)
                                              .FirstOrDefault(s => s.Id == model.WarehouseId)
                                              ?? throw new ArgumentNullException(Exceptions.InvalidWarehouse);

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

        /// <summary>
        /// Delere a parcel by certain ID.
        /// </summary>
        /// <param name="id">ID of the parcel to delete.</param>
        /// <returns>Returns a boolean value if the parcel is deleted.</returns>
        public bool Delete(int id)
        {
            var parcel = FindParcel(id);
            parcel.IsDeleted = true;
            parcel.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();

            return parcel.IsDeleted;
        }

        /// <summary>
        /// Filter and/or sort parcels.
        /// </summary>
        /// <param name="filter1">First filter for parcels.</param>
        /// <param name="value1">Value of the first filter.</param>
        /// <param name="filter2">Second filter for parcels.</param>
        /// <param name="value2">Value of the second filter.</param>
        /// <param name="sortBy1">First sort option for parcels.</param>
        /// <param name="sortBy2">Second sort option for parcels.</param>
        /// <param name="sortingValue">Value of sorting order.</param>
        /// <returns></returns>
        public List<ParcelDTO> GetBy(string filter1, string value1, string filter2, string value2, string sortBy1, string sortBy2, string sortingValue)
        {
            var allParcels = this.dbContext
                            .Parcels
                            .Include(p => p.Category)
                            .Include(p => p.Customer)
                            .Include(p => p.Shipment)
                            .Include(p=>p.Category)
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
                                && (parcel.Customer.FirstName.Equals(value2, StringComparison.OrdinalIgnoreCase) || parcel.Customer.LastName.Equals(value2, StringComparison.OrdinalIgnoreCase) 
                                && parcel.IsDeleted == false)))
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
                                && (parcel.Category.Name.Equals(value2, StringComparison.OrdinalIgnoreCase)) && parcel.IsDeleted == false))
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
                            if ((parcel.Customer.FirstName.Equals(value1, StringComparison.OrdinalIgnoreCase) || parcel.Customer.LastName.Equals(value1, StringComparison.OrdinalIgnoreCase))
                                && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "weight":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Customer.FirstName.Equals(value1, StringComparison.OrdinalIgnoreCase) || parcel.Customer.LastName.Equals(value1, StringComparison.OrdinalIgnoreCase)
                                && (parcel.Weight == double.Parse(value2)) && parcel.IsDeleted == false))
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "warehouse":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Customer.FirstName.Equals(value1, StringComparison.OrdinalIgnoreCase) || parcel.Customer.LastName.Equals(value1, StringComparison.OrdinalIgnoreCase)
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
                            if ((parcel.Customer.FirstName.Equals(value1, StringComparison.OrdinalIgnoreCase) || parcel.Customer.LastName.Equals(value1, StringComparison.OrdinalIgnoreCase)
                                && (parcel.Category.Name.Equals(value2, StringComparison.OrdinalIgnoreCase)) && parcel.IsDeleted == false))
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
                                && (parcel.Customer.FirstName.Equals(value2, StringComparison.OrdinalIgnoreCase) || parcel.Customer.LastName.Equals(value2, StringComparison.OrdinalIgnoreCase)
                                && parcel.IsDeleted == false))
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
                                && (parcel.Category.Name.Equals(value2, StringComparison.OrdinalIgnoreCase)) && parcel.IsDeleted == false)
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
                            if ((parcel.Category.Name.Equals(value1, StringComparison.OrdinalIgnoreCase)) && parcel.IsDeleted == false)
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "weight":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Category.Name.Equals(value1, StringComparison.OrdinalIgnoreCase))
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
                            if ((parcel.Category.Name.Equals(value1, StringComparison.OrdinalIgnoreCase))
                                && (parcel.Customer.FirstName.Equals(value2, StringComparison.OrdinalIgnoreCase) || parcel.Customer.LastName.Equals(value2, StringComparison.OrdinalIgnoreCase)
                                && parcel.IsDeleted == false))
                            {
                                var parcelDTO = new ParcelDTO(parcel);
                                filteredParcels.Add(parcelDTO);
                            }
                        }
                        break;
                    case "warehouse":
                        foreach (var parcel in allParcels)
                        {
                            if ((parcel.Category.Name.Equals(value1, StringComparison.OrdinalIgnoreCase))
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
                throw new ArgumentNullException(Exceptions.InvalidParcels);
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

        /// <summary>
        /// Find a parcel with certain ID.
        /// </summary>
        /// <param name="id">ID of the parcel to get.</param>
        /// <returns>Returns a parcel with certain ID or an appropriate error message.</returns>
        private Parcel FindParcel(int id)
        {
            var parcel = this.dbContext
                             .Parcels
                             .Include(p => p.Category)
                             .Include(p => p.Customer)
                             .Include(s=>s.Shipment)
                             .Include(p => p.Warehouse)
                                .ThenInclude(w => w.Address)
                                    .ThenInclude(a => a.City)
                             .FirstOrDefault(c => c.Id == id)
                             ?? throw new ArgumentNullException(Exceptions.InvalidParcel);

            if (parcel.IsDeleted)
            {
                throw new ArgumentNullException(Exceptions.DeletedParcel);
            }
            return parcel;
        }
    }
}
