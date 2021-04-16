using DeliverIt.Data.Models;

namespace DeliverIt.Services.Models
{
    public class WarehouseDTO
    {
        public WarehouseDTO(Warehouse warehouse)
        {
            this.Address = warehouse.Address.StreetName + ", " + warehouse.Address.City.Name;
        }
        public string Address { get; set; }
    }
}
