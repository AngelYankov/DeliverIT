using DeliverIt.Data.Models;
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
        City City { get; }
        HashSet<Employee> Employees { get; }
        HashSet<Customer> Customers { get; }
        HashSet<Warehouse> Warehouses { get; }
    }
}
