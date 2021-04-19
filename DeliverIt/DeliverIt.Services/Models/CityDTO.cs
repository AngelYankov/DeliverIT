using DeliverIt.Data.Models;
using Newtonsoft.Json;

namespace DeliverIt.Services.Models
{
    public class CityDTO
    {
        public CityDTO(City city)
        {
            City = city.Name;
            Country = city.Country.Name;
            Id = city.Id;
        }
        [JsonIgnore]
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
