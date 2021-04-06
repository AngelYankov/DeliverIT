using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Contracts
{
    public interface ICountry
    {
        int Id { get; }
        string Name { get; }
        HashSet<City> Cities { get; }
    }
}
