using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IParcelService
    {
        Parcel GetById(int id);
        IEnumerable<Parcel> GetAll();
        Parcel Create(Parcel parcel);
        Parcel Update(int id, Parcel parcel);
        bool Delete(int id);
        IEnumerable<Parcel> GetBy(string filter, string type);
    }
}
