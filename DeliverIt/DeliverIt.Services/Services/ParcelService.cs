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
        public Parcel Create(Parcel parcel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Parcel> GetAll()
        {
            return Database.Parcels;
        }

        public Parcel Get(int id)
        {
            var parcel = Database.Parcels.FirstOrDefault(c => c.Id == id);
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
            var parcel = Database.Parcels.FirstOrDefault(c => c.Id == id);

            if (parcel == null)
            {
                throw new ArgumentNullException();
            }
            var customerParcel = Database.Customers.SelectMany(c => c.Parcels).Where(p => p.Id == id);

            parcel.IsDeleted = Database.Parcels.Remove(parcel);

            if (parcel.IsDeleted)
            {
                parcel.DeletedOn = DateTime.UtcNow;
            }
            return parcel.IsDeleted;
        }

        public IEnumerable<Parcel> GetBy(string filter, string type)
        {
            throw new NotImplementedException();
        }
    }
}
