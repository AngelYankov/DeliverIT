using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using System.Collections.Generic;

namespace DeliverIt.Services.Contracts
{
    public interface IAddressService
    {
        AddressDTO Get(int id);
        List<AddressDTO> GetAll();
        AddressDTO Create(Address address);
        Address Update(int id,Address address);

    }
}
