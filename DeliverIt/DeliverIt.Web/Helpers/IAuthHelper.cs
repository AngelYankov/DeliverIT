using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliverIt.Web.Helpers
{
    public interface IAuthHelper
    {
        void TryGetCustomer(string authorization);
        void TryGetEmployee(string authorization);
    }
}
