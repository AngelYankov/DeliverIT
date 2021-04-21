using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Services.Models.Create
{
    public class NewParcelDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int WarehouseId { get; set; }
        public int CategoryId { get; set; }
        public int ShipmentId { get; set; }
        [Range(0.1, 500, ErrorMessage = "Value for {0} should be between {1} and {2} kg.")]
        public double Weight { get; set; }
    }
}
