using DeliverIt.Data.Audit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DeliverIt.Data.Models
{
    public class Address : Entity
    {
        public int Id { get; set; }

        [StringLength(20,MinimumLength = 3, ErrorMessage = "Value for {0} should be between {1} and {2} characters.")]
        public string StreetName { get; set; }

        public int CityID { get; set; }

        public City City { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<Warehouse> Warehouses { get; set; }
    }
}
