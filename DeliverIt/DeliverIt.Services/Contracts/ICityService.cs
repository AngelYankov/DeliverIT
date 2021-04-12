using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface ICityService
    {
        CityDTO Get(int id);
        IList<string> GetAll();
    }
}
