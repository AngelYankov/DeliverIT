﻿using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
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
        EmployeeDTO Update(int id, NewEmployeeDTO model);
        bool Delete(int id);
        //api/customers?searchBy=email&email=abv.bg
        IEnumerable<Employee> SearchBy(string filter, string value);
        //to do filtering
    }
}
