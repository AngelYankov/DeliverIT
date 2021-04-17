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
        public void TryGetCustomer(string authorization)
        {
            try
            {
                this.customerService.GetCustomer(authorization);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
        public void TryGetEmployee(string authorization)
        {
            try
            {
                this.employeeService.GetEmployee(authorization);
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }
    }
}
