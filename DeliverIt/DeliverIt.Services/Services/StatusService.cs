using DeliverIt.Data;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliverIt.Services.Services
{
    public class StatusService : IStatusService
    {
        private readonly DeliverItContext dbContext;

        public StatusService(DeliverItContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Get a status by a certain ID.
        /// </summary>
        /// <param name="id">ID of the status to get.</param>
        /// <returns>Returns a status by a certain ID.</returns>
        public StatusDTO Get(int id)
        {
            var status = dbContext.Statuses.FirstOrDefault(s => s.Id == id)
                ?? throw new ArgumentNullException(Exceptions.InvalidStatus);

            StatusDTO statusDTO = new StatusDTO(status);
            return statusDTO;
        }

        /// <summary>
        /// Get all statuses.
        /// </summary>
        /// <returns>Returns all statuses.</returns>
        public IList<string> GetAll()
        {
            return dbContext.Statuses.Select(s => s.Name).ToList();
        }
    }
}
