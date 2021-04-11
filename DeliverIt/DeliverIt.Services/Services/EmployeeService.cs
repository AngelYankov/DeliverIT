using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DeliverItContext dbContext;
        public EmployeeService(DeliverItContext deliverItContext)
        {
            this.dbContext = deliverItContext;
        }
        public Employee Create(Employee employee)
        {
            dbContext.Employees.Add(employee);
            employee.CreatedOn = DateTime.UtcNow;
            return employee;
        }
        public string Get(int id)
        {
            var employee = dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new ArgumentNullException();
            }
            return employee.FirstName + " " + employee.LastName;
        }

        public IList<string> GetAll()
        {
            return dbContext.Employees.Select(e => e.FirstName + " " + e.LastName).ToList();
        }
        //should we check if the input's addressId is valid?
        public Employee Update(int id, Employee model)
        {
            var employee = dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new ArgumentNullException();
            }
            employee.FirstName = model.FirstName ?? employee.FirstName;
            employee.LastName = model.LastName ?? employee.LastName;
            employee.Email = model.Email ?? employee.Email;
            var modelAddress = dbContext.Addresses.FirstOrDefault(a => a.Id == model.AddressId);
            if (modelAddress == null)
            {
                employee.Address.StreetName = model.Address.StreetName;
                employee.Address.CityID = model.Address.CityID;
            }
            employee.AddressId = model.AddressId;
            employee.ModifiedOn = DateTime.UtcNow;
            return employee;
        }

        public bool Delete(int id)
        {
            var employee = dbContext.Employees.FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new ArgumentNullException();
            }
            dbContext.Employees.Remove(employee);
            return true;
        }

        public IEnumerable<Employee> SearchBy(string filter, string value)
        {
            throw new NotImplementedException();
        }
    }
}
