using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
using System.Collections.Generic;

namespace DeliverIt.Services.Contracts
{
    public interface ICustomerService
    {
        CustomerDTO Get(int id);
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO Create(NewCustomerDTO customer);
        CustomerDTO Update(int id, UpdateCustomerDTO customer);
        bool Delete(int id);
        //api/customers?searchBy=email&email=abv.bg
        IEnumerable<Customer> SearchBy(string filter, string value);
        //to do filtering
    }
}
