using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
using Microsoft.EntityFrameworkCore;
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
        public EmployeeDTO Create(NewEmployeeDTO model)
        {
            var employee = new Employee();
            employee.FirstName = model.FirstName;
            employee.LastName = model.FirstName;
            employee.Email = model.Email;
            employee.AddressId = model.AddressId;
            employee.CreatedOn = DateTime.UtcNow;
            var address = this.dbContext.Addresses
                                        .Include(a => a.City)
                                        .FirstOrDefault(a => a.Id == model.AddressId);
            address.Employees.Add(employee);

            this.dbContext.Employees.Add(employee);
            this.dbContext.SaveChanges();
            var createdEmployee = FindEmployee(employee.Id);
            return new EmployeeDTO(createdEmployee);
        }
        public EmployeeDTO Get(int id)
        {
            var employee = FindEmployee(id);
            return new EmployeeDTO(employee);
        }
        public IEnumerable<EmployeeDTO> GetAll()
        {
            return this.dbContext
                        .Employees
                        .Include(e => e.Address)
                            .ThenInclude(a => a.City)
                        .Where(e => e.IsDeleted == false)
                        .Select(e => new EmployeeDTO(e));
        }
        //TODO change required fields
        public EmployeeDTO Update(int id, UpdateEmployeeDTO model)
        {
            var employee = FindEmployee(id);
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            employee.FirstName = model.FirstName ?? employee.FirstName;
            employee.LastName = model.LastName ?? employee.LastName;
            employee.Email = model.Email ?? employee.Email;
            if (model.AddressId != 0)
            {
                var address = this.dbContext.Addresses
                                    .Include(a => a.City)
                                    .FirstOrDefault(a => a.Id == model.AddressId);
                if (address == null)
                {
                    throw new ArgumentNullException();
                }
                employee.AddressId = model.AddressId;
                employee.ModifiedOn = DateTime.UtcNow;
            }
            this.dbContext.SaveChanges();
            return new EmployeeDTO(employee);
        }
        public bool Delete(int id)
        {
            var employee = FindEmployee(id);
            employee.IsDeleted = true;
            employee.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return true;
        }
        //TODO
        public IEnumerable<Employee> SearchBy(string filter, string value)
        {
            throw new NotImplementedException();
        }
        private Employee FindEmployee(int id)
        {
            var employee = this.dbContext
                               .Employees
                               .Include(e => e.Address)
                                    .ThenInclude(a => a.City)
                               .FirstOrDefault(e => e.Id == id);
            if (employee == null)
            {
                throw new ArgumentNullException();
            }
            if (employee.IsDeleted)
            {
                throw new ArgumentException();
            }
            return employee;
        }
    }
}
