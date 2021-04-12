using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IStatusService
    {
        StatusDTO Get(int id);
        IList<string> GetAll();
    }
}
