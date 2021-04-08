using System.Collections.Generic;

namespace DeliverIt.Data.Models
{
    public class Warehouse 
    {
        public int Id { get; set; }
        public int AddressID { get; set; }
        public Address Address { get; set; }
        public ICollection<Parcel> Parcels { get; set; }
    }
}
