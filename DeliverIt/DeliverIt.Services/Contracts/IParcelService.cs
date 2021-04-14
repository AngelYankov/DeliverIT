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
        IEnumerable<ParcelDTO> GetAll();
        ParcelDTO Create(NewParcelDTO parcel);
        ParcelDTO Update(int id, NewParcelDTO model);
        bool Delete(int id);
        List<ParcelDTO> GetBy(string filter, string value);
    }
}
