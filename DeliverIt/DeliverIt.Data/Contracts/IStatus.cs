using System.Collections.Generic;

namespace DeliverIt.Data.Models
{
    public interface IStatus
    {
        int Id { get; }
        string Name { get; }
        HashSet<Shipment> Shipments { get; }
    }
}