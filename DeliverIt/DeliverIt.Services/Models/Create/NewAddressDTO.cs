using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Services.Models.Create
{
    public class NewAddressDTO
    {
        [Required,StringLength(30, MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string StreetName { get; set; }
        [Required]
        public int CityId { get; set; }
        public int WarehouseId{ get; set; }
    }
}
