using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt.Web.Helpers
{
    public interface IAuthHelper
    {
        Customer TryGetCustomer(string authorization);
        Employee TryGetEmployee(string authorization);
    }
}
