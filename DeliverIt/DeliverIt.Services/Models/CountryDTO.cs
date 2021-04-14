using DeliverIt.Data.Models;

namespace DeliverIt.Services.Models
{
    public class CountryDTO
    {
        public CountryDTO(Country country)
        {
            Name = country.Name;
        }
        public string Name { get; set; }
    }
}
