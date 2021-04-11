using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class ParcelService : IParcelService
    {
        private readonly DeliverItContext dbContext;

        public ParcelService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Parcel Create(Parcel parcel, Customer customer)
        {
            dbContext.Parcels.Add(parcel);
            customer.Parcels.Add(parcel);
            parcel.CreatedOn = DateTime.UtcNow;
            return parcel;
        }

        public IEnumerable<Parcel> GetAll()
        {
            return dbContext.Parcels;
        }

        public Parcel Get(int id)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(c => c.Id == id);
            if (parcel == null)
            {
                throw new ArgumentNullException();
            }
            return parcel;
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
                parcel.CustomerId = model.CustomerId;
            }
            if (model.ShipmentId != 0)
            {
                parcel.ShipmentId = model.ShipmentId;
            }
            if (model.WarehouseId != 0)
            {
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

        public IEnumerable<Parcel> GetBy(string filter, string type)
        {
            throw new NotImplementedException();
        }
    }
}
