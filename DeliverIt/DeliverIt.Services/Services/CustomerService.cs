using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
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
        public Customer Create(Customer customer)
        {
            this.dbContext.Customers.Add(customer);
            customer.CreatedOn = DateTime.UtcNow;
            return customer;
        }

        public string Get(int id)
        {
            var customer = FindCustomer(id);
            return customer.FirstName + " " + customer.LastName;
        }

        public IList<string> GetAll()
        {
            return this.dbContext.Customers.Where(c => c.IsDeleted == false).Select(c => c.FirstName + " " + c.LastName).ToList();
        }

        public Customer Update(int id, Customer model)
        {
            var customer = FindCustomer(id);
            if (model == null)
            {
                throw new ArgumentNullException();
            }
            customer = model;
            customer.ModifiedOn = DateTime.UtcNow;
            return customer;
        }

        public bool Delete(int id)
        {
            var customer = FindCustomer(id);
            customer.IsDeleted = true;
            customer.DeletedOn = DateTime.UtcNow;
            return true;
        }
        //TODO
        public IEnumerable<Customer> SearchBy(string filter, string value)
        {
            throw new NotImplementedException();
        }
        private Customer FindCustomer(int id)
        {
            var customer = this.dbContext
                               .Customers
                               .Include(c => c.Address)
                               .FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new ArgumentNullException();
            }
            if (customer.IsDeleted)
            {
                throw new ArgumentException();
            }
            return customer;
        }
    }
}
