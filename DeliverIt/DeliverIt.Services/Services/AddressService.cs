using DeliverIt.Data;
using DeliverIt.Data.Models;
using DeliverIt.Services.Contracts;
using DeliverIt.Services.Models;
using DeliverIt.Services.Models.Create;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliverIt.Services.Services
{
    public class AddressService : IAddressService
    {
        private readonly DeliverItContext dbcontext;

        public AddressService(DeliverItContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        /// <summary>
        /// Create an address.
        /// </summary>
        /// <param name="model">Details of the address to be created.</param>
        /// <returns>Returns created address or an appropriate error message.</returns>
        public AddressDTO Create(NewAddressDTO model)
        {
            var city = this.dbcontext.Cities.FirstOrDefault(c => c.Id == model.CityId)
                ?? throw new ArgumentException(Exceptions.InvalidCity);
            var address = new Address();
            address.StreetName = model.StreetName;
            address.CityID = model.CityId;
            address.CreatedOn = DateTime.UtcNow;
            city.Addresses.Add(address);
            this.dbcontext.Addresses.Add(address);
            this.dbcontext.SaveChanges();
            return new AddressDTO(address);
        }
        /// <summary>
        /// Get an address by a certain ID
        /// </summary>
        /// <param name="id">ID of the address to get</param>
        /// <returns>Returns an address or an appropriate error message.</returns>
        public AddressDTO Get(int id)
        {
            var address = FindAddress(id);
            return new AddressDTO(address);
        }
        /// <summary>
        /// Get an Address by ID
        /// </summary>
        /// <param name="id">Id to search for.</param>
        /// <returns>Returns the address with that Id.</returns>
        public Address GetBaseForTest(int id)
        {
            return FindAddress(id);
        }
        /// <summary>
        /// Get all addresses.
        /// </summary>
        /// <returns>Returns all addresses.</returns>
        public IEnumerable<AddressDTO> GetAll()
        {
            return this.dbcontext
                       .Addresses
                       .Include(a => a.City)
                       .Select(a => new AddressDTO(a));
        }
        /// <summary>
        /// Update an address by a certain ID and data.
        /// </summary>
        /// <param name="id">ID of the address to update.</param>
        /// <param name="model">Details of the address to be updated.</param>
        /// <returns>Returns the updated address or an appropriate error message. </returns>
        public AddressDTO Update(int id, NewAddressDTO model)
        {
            var address = FindAddress(id);
            address.StreetName = model.StreetName ?? address.StreetName;
            if (model.CityId != 0)
            {
                var city = this.dbcontext
                               .Cities
                               .FirstOrDefault(c => c.Id == model.CityId)
                               ?? throw new ArgumentException(Exceptions.InvalidCity);
                address.CityID = model.CityId;
            }
            var warehouse = this.dbcontext.Warehouses.Include(w => w.Address)
                                                     .FirstOrDefault(w => w.Id == model.WarehouseId)
                                                     ?? throw new ArgumentException(Exceptions.InvalidWarehouse);

            address.Warehouse = warehouse;
            address.ModifiedOn = DateTime.UtcNow;
            this.dbcontext.SaveChanges();
            return new AddressDTO(address);
        }
        /// <summary>
        /// Find an adress by ID
        /// </summary>
        /// <param name="id">ID of the address to search for</param>
        /// <returns>Returns address with that ID</returns>
        private Address FindAddress(int id)
        {
            var address = this.dbcontext
                              .Addresses
                              .Include(a => a.City)
                              .FirstOrDefault(a => a.Id == id)
                              ?? throw new ArgumentException(Exceptions.InvalidAddress);
            return address;
        }
    }
}
