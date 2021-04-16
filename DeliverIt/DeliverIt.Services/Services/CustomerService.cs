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
        public CustomerDTO Get(int id)
        {
            var customer = FindCustomer(id);
            return new CustomerDTO(customer);
        }
        public IEnumerable<CustomerDTO> GetAll()
        {
            return this.dbContext
                       .Customers
                       .Include(c => c.Address)
                            .ThenInclude(a => a.City)
                       .Where(c => c.IsDeleted == false)
                       .Select(c => new CustomerDTO(c));
        }
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
                                ?? throw new ArgumentException("There is no such address.");
        }
        private Customer FindCustomer(int id)
        {
            var customer = this.dbContext
                               .Customers
                               .Include(c => c.Address)
                                    .ThenInclude(a => a.City)
                               .FirstOrDefault(c => c.Id == id)
                               ?? throw new ArgumentException("There is no such customer.");
            if (customer.IsDeleted)
            {
                throw new ArgumentException("Customer has been already deleted.");
            }
            return customer;
        }
    }
}
