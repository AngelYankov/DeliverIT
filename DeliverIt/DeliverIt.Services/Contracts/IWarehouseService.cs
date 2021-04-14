using DeliverIt.Data.Models;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IWarehouseService
    {
        WarehouseDTO Get(int id);
        IEnumerable<WarehouseDTO> GetAll();
        WarehouseDTO Create(NewWarehouseDTO warehouse);
        WarehouseDTO Update(int id, NewWarehouseDTO model);
        bool Delete(int id);
    }
}
