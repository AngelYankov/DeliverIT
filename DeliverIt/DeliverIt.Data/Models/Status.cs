using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Data.Models
{
    public class Status 
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(25, MinimumLength = 5, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Name { get; set; }
        public ICollection<Shipment> Shipments { get; set; } = new HashSet<Shipment>();
    }
}