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
        public Parcel Create(Parcel parcel)
        {
            throw new NotImplementedException();
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

        public Parcel Update(int id, Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            var parcel = dbContext.Parcels.FirstOrDefault(c => c.Id == id);

            if (parcel == null)
            {
                throw new ArgumentNullException();
            }
            var customerParcel = dbContext.Customers.SelectMany(c => c.Parcels).Where(p => p.Id == id);

            dbContext.Parcels.Remove(parcel);
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
