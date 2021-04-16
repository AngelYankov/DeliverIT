using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
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
        ParcelDTO Update(int id, UpdateParcelDTO model);
        bool Delete(int id);
        List<ParcelDTO> GetBy(string filter1, string value1, string filter2, string value2, string sortBy1, string sortBy2, string sortingValue);
    }
}
