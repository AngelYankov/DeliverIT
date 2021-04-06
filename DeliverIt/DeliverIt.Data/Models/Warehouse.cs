using DeliverIt.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Warehouse : IWarehouse
    {
        public int Id { get; set; }
        public int AddressID { get; set; }
        public Address Address { get; set; }
        public HashSet<Parcel> Parcels { get; set; }
    }
}
