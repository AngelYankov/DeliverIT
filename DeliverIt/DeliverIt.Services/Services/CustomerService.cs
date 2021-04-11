using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
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
            throw new NotImplementedException();
        }

        public string Get(int id)
        {
            var customer = dbContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                throw new ArgumentNullException();
            }
            return customer.FirstName + " " + customer.LastName;
        }

        public IList<string> GetAll()
        {
            return dbContext.Customers.Select(c => c.FirstName + " " + c.LastName).ToList();
        }

        public Customer Update(int id, Customer customer)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> SearchBy(string filter, string value)
        {
            throw new NotImplementedException();
        }
    }
}
