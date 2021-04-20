using DeliverIt.Data.Models;
using Newtonsoft.Json;

namespace DeliverIt.Services.Models
{
    public class StatusDTO
    {
        public StatusDTO(Status status)
        {
            Status = status.Name;
            Id = status.Id;
        }
        [JsonIgnore]
        public int Id { get; set; }
        
        public string Status { get; set; }
    }
}
