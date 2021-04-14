using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Services.Models.Create
{
    public class NewCustomerDTO
    {
        [Required, StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string FirstName { get; set; }

        [Required, StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public int AddressId { get; set; }

    }
}
