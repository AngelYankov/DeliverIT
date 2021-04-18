using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using DeliverIt.Services.Models.Update;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IEmployeeService
    {
        EmployeeDTO Get(int id);
        IEnumerable<EmployeeDTO> GetAll();
        EmployeeDTO Create(NewEmployeeDTO model);
        EmployeeDTO Update(int id, UpdateEmployeeDTO model);
        bool Delete(int id);
        Employee GetEmployee(string username);
        IEnumerable<EmployeeDTO> SearchBy(string filter, string value,string order);
    }
}
