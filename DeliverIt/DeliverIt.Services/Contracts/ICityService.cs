using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface ICityService
    {
        string Get(int id);
        IList<string> GetAll();
    }
}
