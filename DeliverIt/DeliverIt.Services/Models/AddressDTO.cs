using DeliverIt.Data.Models;

namespace DeliverIt.Services.Models
{
    public class AddressDTO
    {
        public AddressDTO(Address address)
        {
            this.StreetAddress = address.StreetName+","+ address.City.Name;
        }
        public string StreetAddress { get; set; }
        
    }
}
