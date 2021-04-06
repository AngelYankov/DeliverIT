using System.Collections.Generic;

namespace DeliverIt.Data.Models
{
    public interface ICity
    {
        int Id { get; }
        string Name { get; }
        int CountryId { get; }
        Country Country { get; }
        HashSet<Address> Addresses { get; }
    }
}