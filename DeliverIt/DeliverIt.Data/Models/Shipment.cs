using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Shipment 
    {
        public int Id { get; set; }
        [DataType(DataType.Date, ErrorMessage = "Please enter a correct date format dd/mm/yyyy hh:mm"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public ICollection<Parcel> Parcels { get; set; }
    }
}
