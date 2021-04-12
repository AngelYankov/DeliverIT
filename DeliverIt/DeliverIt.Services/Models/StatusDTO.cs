using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
