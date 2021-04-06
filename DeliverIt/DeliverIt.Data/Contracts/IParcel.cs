namespace DeliverIt.Data.Models
{
    public interface IParcel
    {
        int Id { get; }
        int CustomerId { get; }
        Customer Customer { get; }
        int WarehouseId { get; }
        Warehouse Warehouse { get; }
        double Weight { get; }
        int CategoryId { get; }
        Category Category { get; }
        int ShipmentId { get; }
        Shipment Shipment { get; }
    }
}