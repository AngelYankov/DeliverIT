using System;
using System.Collections.Generic;

namespace DeliverIt.Data.Models
{
    public interface IShipment
    {
        int Id { get; set; }
        DateTime Departure { get; set; }
        DateTime Arrival { get; set; }
        int StatusId { get; set; }
        Status Status { get; set; }
        HashSet<Parcel> Parcels { get; set; }
    }
}