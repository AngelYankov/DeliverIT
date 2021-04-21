using DeliverIt.Data.Audit;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeliverIt.Data.Models
{
    public class Address : Entity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(30,MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string StreetName { get; set; }
        public int CityID { get; set; }
        public City City { get; set; }
        public Warehouse Warehouse { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
        public ICollection<Customer> Customers { get; set; } = new HashSet<Customer>();
    }
}
