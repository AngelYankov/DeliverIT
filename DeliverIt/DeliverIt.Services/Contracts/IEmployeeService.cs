using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IEmployeeService
    {
        string Get(int id);
        IList<string> GetAll();
        Employee Create(Employee model);
        Employee Update(int id, Employee model);
        bool Delete(int id);
        //api/customers?searchBy=email&email=abv.bg
        IEnumerable<Employee> SearchBy(string filter, string value);
        //to do filtering
    }
}
