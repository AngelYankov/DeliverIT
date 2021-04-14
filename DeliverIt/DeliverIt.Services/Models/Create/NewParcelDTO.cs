using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Services.Models.Create
{
    public class NewParcelDTO
    {
        public int CustomerId { get; set; }
        public int WarehouseId { get; set; }
        public int CategoryId { get; set; }
        public int ShipmentId { get; set; }
        [Required, Range(0.1, 500, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public double Weight { get; set; }
    }
}
