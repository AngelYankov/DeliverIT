using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Services.Models.Update
{
    public class UpdateCustomerDTO
    {
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string FirstName { get; set; }
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public int AddressId { get; set; }
    }
}
