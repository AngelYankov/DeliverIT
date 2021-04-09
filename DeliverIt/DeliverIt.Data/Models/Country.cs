using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Country : Entity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; } = new HashSet<City>();
    }
}
