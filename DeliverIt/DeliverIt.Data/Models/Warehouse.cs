using DeliverIt.Data.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Data.Models
{
    public class Warehouse : Entity
    {
        [Key]
        public int Id { get; set; }
        public int AddressID { get; set; }
        public Address Address { get; set; }
        public ICollection<Parcel> Parcels { get; set; } = new HashSet<Parcel>();
    }
}
