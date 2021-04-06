using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Contracts
{
    public interface IWarehouse
    {
        int Id { get; }
        int AddressID { get; }
        Address Address { get; }
        HashSet<Parcel> Parcels { get; }
    }
}
