using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IParcelService
    {
        ParcelDTO Get(int id);
        List<ParcelDTO> GetAll();
        ParcelDTO Create(NewParcelDTO parcel);
        Parcel Update(int id, Parcel parcel);
        bool Delete(int id);
        List<ParcelDTO> GetBy(string filter, string value);
    }
}
