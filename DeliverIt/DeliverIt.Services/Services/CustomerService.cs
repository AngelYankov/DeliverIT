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

namespace DeliverIt.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DeliverItContext dbContext;
        private readonly IAddressService addressService;

        public CustomerService(DeliverItContext dbContext, IAddressService addressService)
        {
            this.dbContext = dbContext;
            this.addressService = addressService;
        }
        /// <summary>
        /// Create a customer
        /// </summary>
        /// <param name="model">Data of the customer to be created.</param>
        /// <returns>Returns new customer or an appropriate error message.</returns>
        public CustomerDTO Create(NewCustomerDTO model)
        {
            var address = this.addressService.GetBaseForTest(model.AddressId);
            var customer = new Customer();
            address.Customers.Add(customer);
            customer.FirstName = model.FirstName;
            customer.LastName = model.LastName;
            customer.Email = model.Email;
            customer.AddressId = model.AddressId;
            customer.CreatedOn = DateTime.UtcNow;

            this.dbContext.Customers.Add(customer);
            this.dbContext.SaveChanges();
            var createdCustomer = FindCustomer(customer.Id);
            return new CustomerDTO(createdCustomer);
        }
        /// <summary>
        /// Get customer by ID
        /// </summary>
        /// <param name="id">ID to search for</param>
        /// <returns>Returns customer with that ID or an appropriate error message.</returns>
        public CustomerDTO Get(int id)
        {
            var customer = FindCustomer(id);
            return new CustomerDTO(customer);
        }
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>Returns all customers or an appropriate error message.</returns>
        public IEnumerable<CustomerDTO> GetAll()
        {
            return this.dbContext
                       .Customers
                       .Include(c => c.Address)
                            .ThenInclude(a => a.City)
                       .Where(c => c.IsDeleted == false)
                       .Select(c => new CustomerDTO(c));
        }
        /// <summary>
        /// Update a customer.
        /// </summary>
        /// <param name="id">ID of customer to search for.</param>
        /// <param name="model">Data to be updated.</param>
        /// <returns>Returns updated customer or an appropriate error message.</returns>
        public CustomerDTO Update(int id, UpdateCustomerDTO model)
        {
            var customer = FindCustomer(id);
            customer.FirstName = model.FirstName ?? customer.FirstName;
            customer.LastName = model.LastName ?? customer.LastName;
            customer.Email = model.Email ?? customer.Email;
            if (model.AddressId != 0)
            {
                this.addressService.GetBaseForTest(model.AddressId);
                customer.AddressId = model.AddressId;
                customer.ModifiedOn = DateTime.UtcNow;
            }
            this.dbContext.SaveChanges();
            return new CustomerDTO(customer);
        }
        /// <summary>
        /// Delete a customer.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns true or false if succesful or an appropriate error message.</returns>
        public bool Delete(int id)
        {
            var customer = FindCustomer(id);
            customer.IsDeleted = true;
            customer.DeletedOn = DateTime.UtcNow;
            dbContext.SaveChanges();
            return customer.IsDeleted;
        }
        /// <summary>
        /// Get filtered customers.
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="value">Value of the filter</param>
        /// <param name="order">Asc or Desc</param>
        /// <returns>Returns all filtered customers.</returns>
        public IEnumerable<CustomerDTO> SearchBy(string filter, string value,string filter2,string value2, string order)
        {
            var allCustomers = this.dbContext.Customers
                                             .Include(c => c.Address)
                                             .ThenInclude(a => a.City)
                                             .Where(c => c.IsDeleted == false)
                                             .ToList();
            var filtered = new List<Customer>();
            switch (filter)
            {
                case "firstName":
                    var temp = new List<Customer>().AsEnumerable();
                    if (filter2 == "lastName")
                    {
                        temp = allCustomers.Where(c => c.IsDeleted == false && c.FirstName.Equals(value, StringComparison.OrdinalIgnoreCase) && c.LastName.Equals(value2, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (filter2==null)
                    {
                        temp = allCustomers.Where(c => c.IsDeleted == false && c.FirstName.Equals(value, StringComparison.OrdinalIgnoreCase));
                    }
                    if (order == "desc")
                    {
                        filtered.AddRange(temp.OrderByDescending(c=>c.FirstName).ThenByDescending(c=>c.LastName));
                    }
                    else filtered.AddRange(temp.OrderBy(c => c.FirstName).ThenBy(c=>c.LastName));
                    break;
                case "lastName":
                    var temp2 = new List<Customer>().AsEnumerable();
                    if (filter2 == "firstName")
                    {
                        temp2 = allCustomers.Where(c => c.IsDeleted == false && c.FirstName.Equals(value2, StringComparison.OrdinalIgnoreCase) && c.LastName.Equals(value, StringComparison.OrdinalIgnoreCase));
                    }
                    else if (filter2 == null)
                    {
                        temp2 = allCustomers.Where(c => c.IsDeleted == false && c.LastName.Equals(value, StringComparison.OrdinalIgnoreCase));
                    }
                    if (order == "desc")
                    {
                        filtered.AddRange(temp2.OrderByDescending(c => c.LastName).ThenByDescending(c=>c.FirstName));
                    }
                    else filtered.AddRange(temp2.OrderBy(c => c.LastName).ThenBy(c=>c.FirstName));
                    break;
                case "email":
                    var temp3 = allCustomers.Where(c => c.IsDeleted == false && c.Email != null && c.Email.Contains(value, StringComparison.OrdinalIgnoreCase));
                    if (order == "desc")
                    {
                        filtered.AddRange(temp3.OrderByDescending(c => c.Email));
                    }
                    else filtered.AddRange(temp3.OrderBy(c => c.Email));
                    break;
                default: throw new ArgumentException(Exceptions.InvalidCustomer);
            }
            if (filtered.Count()==0)
            {
                throw new ArgumentException(Exceptions.InvalidFilteredCustomers);
            }
            return filtered.Select(c => new CustomerDTO(c));
        }
        /// <summary>
        /// Finds a customer with ID.
        /// </summary>
        /// <param name="id">ID to search for.</param>
        /// <returns>Returns customer or an appropriate error message.</returns>
        private Customer FindCustomer(int id)
        {
            var customer = this.dbContext
                               .Customers
                               .Include(c => c.Address)
                                    .ThenInclude(a => a.City)
                               .FirstOrDefault(c => c.Id == id)
                               ?? throw new ArgumentException(Exceptions.InvalidCustomer);
            if (customer.IsDeleted)
            {
                throw new ArgumentException(Exceptions.DeletedCustomer);
            }
            return customer;
        }

        /// <summary>
        /// Get a customer with certain username
        /// </summary>
        /// <param name="username">Username to check for</param>
        public Customer GetCustomer(string username)
        {
            return this.dbContext
                .Customers
                .Where(c => c.IsDeleted == false)
                .FirstOrDefault(c => (c.FirstName + "." + c.LastName).ToLower() == username)
                ?? throw new ArgumentException(Exceptions.InvalidUsername);
        }

        /// <summary>
        /// Get all customers count
        /// </summary>
        /// <returns>Returns the number of customers.</returns>
        public int GetAllCount()
        {
            var allCustomersCount = this.dbContext.Customers.Count();

            return allCustomersCount;
        }
    }
}
