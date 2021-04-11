using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(25, MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Address> Addresses { get; set; } = new HashSet<Address>();
    }
}
