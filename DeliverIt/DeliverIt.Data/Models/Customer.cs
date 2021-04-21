using DeliverIt.Data.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Data.Models
{
    public class Customer : Entity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string FirstName { get; set; }

        [Required, StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string LastName { get; set; }

        [Required,EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<Parcel> Parcels { get; set; } = new HashSet<Parcel>();
    }
}
