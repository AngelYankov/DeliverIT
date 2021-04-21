using DeliverIt.Data.Models;

namespace DeliverIt.Web.Helpers
{
    public interface IAuthHelper
    {
        Customer TryGetCustomer(string authorization);
        Employee TryGetEmployee(string authorization);
    }
}
