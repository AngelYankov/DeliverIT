using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IParcelService
    {
        ParcelDTO Get(int id);
        IEnumerable<Parcel> GetAll();
        Parcel Create(Parcel parcel, Customer customer);
        Parcel Update(int id, Parcel parcel);
        bool Delete(int id);
        IEnumerable<Parcel> GetBy(string filter, string type);
    }
}
