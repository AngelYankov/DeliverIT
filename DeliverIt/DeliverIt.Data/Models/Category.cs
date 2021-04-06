using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Category : ICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public HashSet<Parcel> Parcels { get; set; }
    }
}
