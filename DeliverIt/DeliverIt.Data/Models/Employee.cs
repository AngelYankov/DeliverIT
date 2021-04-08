using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Employee : Entity
    {
        public int Id { get; set; }
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters")]
        public string FirstName { get; set; }
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Value for {0} should be between {1} and {2} characters")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
