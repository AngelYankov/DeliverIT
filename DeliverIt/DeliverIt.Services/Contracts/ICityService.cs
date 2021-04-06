using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface ICityService
    {
        City Get(int id);
        IEnumerable<City> GetAll();
    }
}
