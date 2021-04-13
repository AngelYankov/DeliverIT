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
        List<ParcelDTO> GetAll();
        Parcel Create(Parcel parcel);
        Parcel Update(int id, Parcel parcel);
        bool Delete(int id);
        List<ParcelDTO> GetBy(string filter, string value);
    }
}
