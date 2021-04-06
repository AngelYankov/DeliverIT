using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Data.Contracts
{
    public interface IAddress
    {
        int Id { get; }
        string StreetName { get; }
        int CityID { get; }
    }
}
