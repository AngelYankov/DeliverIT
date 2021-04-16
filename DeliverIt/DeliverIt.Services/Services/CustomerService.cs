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
    public class CustomerService : ICustomerService
    {
        private readonly DeliverItContext dbContext;

        public CustomerService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Create a customer
        /// </summary>
        /// <param name="model">Data of the customer to be created.</param>
        /// <returns>Returns new customer or an appropriate error message.</returns>
        public CustomerDTO Create(NewCustomerDTO model)
        {
            var address = FindAddress(model.AddressId);
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
                FindAddress(model.AddressId);
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
            return true;
        }
        //TODO
        public IEnumerable<Customer> SearchBy(string filter, string value)
        {
            throw new NotImplementedException();
        }
        private Address FindAddress(int id)
        {
            return this.dbContext.Addresses
                                .Include(a => a.City)
                                .FirstOrDefault(a => a.Id == id)
                                ?? throw new ArgumentException(Exceptions.InvalidAddress);
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
    }
}
