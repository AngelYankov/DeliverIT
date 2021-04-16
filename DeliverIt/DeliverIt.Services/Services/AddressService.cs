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
        /// Creates new Address.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
        public AddressDTO Get(int id)
        {
            var address = FindAddress(id);
            return new AddressDTO(address);
        }
        public IEnumerable<AddressDTO> GetAll()
        {
            return this.dbcontext
                       .Addresses
                       .Include(a => a.City)
                       .Select(a => new AddressDTO(a));
        }
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
            address.ModifiedOn = DateTime.UtcNow;
            this.dbcontext.SaveChanges();
            return new AddressDTO(address);
        }
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
