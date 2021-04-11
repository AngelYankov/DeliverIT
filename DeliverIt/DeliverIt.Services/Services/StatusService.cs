using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class StatusService : IStatusService
    {
        private readonly DeliverItContext dbContext;

        public StatusService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public string Get(int id)
        {
            var status = dbContext.Statuses.FirstOrDefault(s => s.Id == id).Name;
            if(status == null)
            {
                throw new ArgumentNullException();
            }
            return status;
        }

        public IList<string> GetAll()
        {
            return dbContext.Statuses.Select(s => s.Name).ToList();
        }
    }
}
