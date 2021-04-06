using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Contracts
{
    public interface ICustomer
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        int AddressId { get; }
        Address Address { get; }
        HashSet<Parcel> Parcels { get; }
    }
}
