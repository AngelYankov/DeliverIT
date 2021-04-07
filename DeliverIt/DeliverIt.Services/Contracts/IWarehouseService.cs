﻿using DeliverIt.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliverIt.Services.Contracts
{
    public interface IWarehouseService
    {
        Warehouse Get(int id);
        IEnumerable<Warehouse> GetAll();
        // to do CRUD
    }
}