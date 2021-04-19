using DeliverIt.Data.Models;
using Newtonsoft.Json;

namespace DeliverIt.Services.Models
{
    public class CountryDTO
    {
        public CountryDTO(Country country)
        {
            Name = country.Name;
            Id = country.Id;
        }
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
