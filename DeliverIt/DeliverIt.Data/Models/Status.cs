using DeliverIt.Data.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Data.Models
{
    public class Status : Entity
    {
        public int Id { get; set; }
        [StringLength(25, MinimumLength = 5, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Name { get; set; }
        public ICollection<Shipment> Shipments { get; set; }
    }
}