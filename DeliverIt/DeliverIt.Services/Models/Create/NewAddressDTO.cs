using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Services.Models.Create
{
    public class NewAddressDTO
    {
        [Required, StringLength(30, MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string StreetName { get; set; }
        public int CityId { get; set; }
    }
}
