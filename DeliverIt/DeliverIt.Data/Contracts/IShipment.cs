using System;
using System.Collections.Generic;

namespace DeliverIt.Data.Models
{
    public interface IShipment
    {
        int Id { get; }
        DateTime Departure { get; }
        DateTime Arrival { get; }
        int StatusId { get; }
        Status Status { get; }
        HashSet<Parcel> Parcels { get; }
    }
}