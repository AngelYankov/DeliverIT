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
        public Customer Create(Customer customer)
        {
            throw new NotImplementedException();
        }

        public string Get(int id)
        {
            var customer = Database.Customers.FirstOrDefault(c => c.Id == id).FirstName + " " + Database.Customers.FirstOrDefault(c => c.Id == id).FirstName;
            if(customer == null)
            {
                throw new ArgumentNullException();
            }
            return customer;
        }

        public IList<string> GetAll()
        {
            return Database.Customers.Select(c => c.FirstName + " " + c.LastName).ToList();
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
