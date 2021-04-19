using DeliverIt.Data.Models;
using Newtonsoft.Json;

namespace DeliverIt.Services.Models
{
    public class AddressDTO
    {
        public AddressDTO(Address address)
        {
            this.Address = address;
            this.Id = address.Id;
            this.StreetAddress = address.StreetName+","+ address.City.Name;
        }
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public Address Address { get; set; }
        public string StreetAddress { get; set; }
        
    }
}
