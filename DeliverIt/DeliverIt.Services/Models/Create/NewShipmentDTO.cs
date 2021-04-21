using Newtonsoft.Json;

namespace DeliverIt.Services.Models.Create
{
    public class NewShipmentDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int WarehouseId { get; set; }
    }
}
