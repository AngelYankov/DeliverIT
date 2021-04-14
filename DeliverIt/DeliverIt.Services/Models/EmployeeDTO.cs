using DeliverIt.Data.Models;

namespace DeliverIt.Services.Models
{
    public class EmployeeDTO
    {
        public EmployeeDTO(Employee employee)
        {
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.Email = employee.Email;
            this.Address = employee.Address.StreetName + ", " + employee.Address.City.Name;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

    }
}
