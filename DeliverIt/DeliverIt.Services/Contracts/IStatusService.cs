using DeliverIt.Services.Models;
using System.Collections.Generic;

namespace DeliverIt.Services.Contracts
{
    public interface IStatusService
    {
        StatusDTO Get(int id);
        IList<string> GetAll();
    }
}
