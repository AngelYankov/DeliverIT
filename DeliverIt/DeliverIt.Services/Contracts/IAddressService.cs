using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using System.Collections.Generic;

namespace DeliverIt.Services.Contracts
{
    public interface IAddressService
    {
        AddressDTO Get(int id);
        IEnumerable<AddressDTO> GetAll();
        AddressDTO Create(NewAddressDTO address);
        AddressDTO Update(int id,NewAddressDTO address);
        Address GetBaseForTest(int id);
    }
}
