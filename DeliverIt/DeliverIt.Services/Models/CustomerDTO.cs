using DeliverIt.Data.Models;

namespace DeliverIt.Services.Models
{
    public class CustomerDTO
    {
        public CustomerDTO(Customer customer)
        {
            this.FirstName = customer.FirstName;
            this.LastName = customer.LastName;
            this.Email = customer.Email;
            this.Address = customer.Address.StreetName + ", " + customer.Address.City.Name;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
