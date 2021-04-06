using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IStatusService
    {
        Status Get(int id);
        IEnumerable<Status> GetAll();
    }
}
