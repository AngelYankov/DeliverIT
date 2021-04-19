using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt.Web.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        private readonly ICustomerService customerService;
        private readonly IEmployeeService employeeService;

        public AuthHelper(ICustomerService customerService, IEmployeeService employeeService)
        {
            this.customerService = customerService;
            this.employeeService = employeeService;
        }
        public Customer TryGetCustomer(string authorization)
        {
            try
            {
                var customer = this.customerService.GetCustomer(authorization);
                return customer;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        public Employee TryGetEmployee(string authorization)
        {
            try
            {
                var employee = this.employeeService.GetEmployee(authorization);
                return employee;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
