using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Parcel : IParcel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public int WarehouseId { get; set; }
        public double Weight { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
