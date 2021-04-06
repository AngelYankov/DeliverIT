using System.Collections.Generic;

namespace DeliverIt.Data.Models
{
    public interface ICategory
    {
        int Id { get; }
        string Name { get; }
        HashSet<Parcel> Parcels { get; }
    }
}