namespace DeliverIt.Data.Models
{
    public interface IParcel
    {
        int Id { get; set; }
        int CustomerId { get; set; }
        Customer Customer { get; set; }
        int WarehouseId { get; set; }
        double Weight { get; set; }
        int CategoryId { get; set; }
        Category Category { get; set; }
    }
}