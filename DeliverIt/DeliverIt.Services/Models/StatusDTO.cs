using DeliverIt.Data.Models;

namespace DeliverIt.Services.Models
{
    public class StatusDTO
    {
        public StatusDTO(Status status)
        {
            Status = status.Name;
        }
        public string Status { get; set; }
    }
}
