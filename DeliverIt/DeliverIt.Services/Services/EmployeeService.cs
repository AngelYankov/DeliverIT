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
        /// <summary>
        /// Creae an employee
        /// </summary>
        /// <param name="model">Data of the employee to be created.</param>
        /// <returns>Returns new employee or an appropriate error message.</returns>
        public EmployeeDTO Create(NewEmployeeDTO model)
        {
            var address = FindAddress(model.AddressId);
            var employee = new Employee();
            employee.FirstName = model.FirstName;
            employee.LastName = model.FirstName;
            employee.Email = model.Email;
            employee.AddressId = model.AddressId;
            employee.CreatedOn = DateTime.UtcNow;
            address.Employees.Add(employee);
            this.dbContext.Employees.Add(employee);
            this.dbContext.SaveChanges();
            var createdEmployee = FindEmployee(employee.Id);
            return new EmployeeDTO(createdEmployee);
        }
        /// <summary>
        /// Get an employee.
        /// </summary>
        /// <param name="id">ID to search for</param>
        /// <returns>Returns employee with that ID or an appropriate error message.</returns>
        public EmployeeDTO Get(int id)
        {
            var employee = FindEmployee(id);
            return new EmployeeDTO(employee);
        }
        /// <summary>
        /// Get all employees.
        /// </summary>
        /// <returns>Returns all employees or an appropriate error message.</returns>
        public IEnumerable<EmployeeDTO> GetAll()
        {
            return this.dbContext
                        .Employees
                        .Include(e => e.Address)
                            .ThenInclude(a => a.City)
                        .Where(e => e.IsDeleted == false)
                        .Select(e => new EmployeeDTO(e));
        }
        /// <summary>
        /// Update an employee.
        /// </summary>
        /// <param name="id">ID of employee to search for.</param>
        /// <param name="model">Data to be updated.</param>
        /// <returns></returns>
        public EmployeeDTO Update(int id, UpdateEmployeeDTO model)
        {
            var employee = FindEmployee(id);
            employee.FirstName = model.FirstName ?? employee.FirstName;
            employee.LastName = model.LastName ?? employee.LastName;
            employee.Email = model.Email ?? employee.Email;
            if (model.AddressId != 0)
            {
                FindAddress(model.AddressId);
                employee.AddressId = model.AddressId;
                employee.ModifiedOn = DateTime.UtcNow;
            }
            this.dbContext.SaveChanges();
            return new EmployeeDTO(employee);
        }
        /// <summary>
        /// Delete an employee.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns true or false if successful or an appropriate error message.</returns>
        public bool Delete(int id)
        {
            var employee = FindEmployee(id);
            employee.IsDeleted = true;
            employee.DeletedOn = DateTime.UtcNow;
            this.dbContext.SaveChanges();
            return employee.IsDeleted;
        }
        //TODO
        public IEnumerable<Employee> SearchBy(string filter, string value)
        {
            throw new NotImplementedException();
        }
        private Address FindAddress(int id)
        {
            return this.dbContext.Addresses
                                .Include(a => a.City)
                                .FirstOrDefault(a => a.Id == id)
                                ?? throw new ArgumentException(Exceptions.InvalidAddress);
        }/// <summary>
         /// Find an employee with ID.
         /// </summary>
         /// <param name="id">ID to search for.</param>
         /// <returns>Returns employee with that ID or an appropriate error message.</returns>
        private Employee FindEmployee(int id)
        {
            var employee = this.dbContext
                               .Employees
                               .Include(e => e.Address)
                                    .ThenInclude(a => a.City)
                               .FirstOrDefault(e => e.Id == id)
                               ?? throw new ArgumentException(Exceptions.InvalidEmployee);
            if (employee.IsDeleted)
            {
                throw new ArgumentException(Exceptions.DeletedEmployee);
            }
            return employee;
        }
        /// <summary>
        /// Get an employee with certain username
        /// </summary>
        /// <param name="username">Username to check for</param>
        public Employee GetEmployee(string username)
        {
            var employee = this.dbContext
                .Employees
                .Where(c => c.IsDeleted == false)
                .FirstOrDefault(c => (c.FirstName + "." + c.LastName).ToLower() == username)
                ?? throw new ArgumentException(Exceptions.InvalidUsername);
            return employee;
        }
    }
}
