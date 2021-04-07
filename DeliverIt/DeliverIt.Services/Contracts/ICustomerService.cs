using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface ICustomerService
    {
        Customer Get(int id);
        IEnumerable<Customer> GetAll();
        Customer Create(Customer customer);
        Customer Update(int id, Customer customer);
        bool Delete(int id);
        //api/customers?searchBy=email&email=abv.bg
        IEnumerable<Customer> SearchBy(string filter, string value);
        //to do filtering
    }
}
