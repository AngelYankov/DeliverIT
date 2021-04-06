namespace DeliverIt.Data.Models
{
    public interface ICity
    {
        int Id { get; set; }
        string Name { get; set; }
        int CountryId { get; set; }
        Country Country { get; set; }
    }
}