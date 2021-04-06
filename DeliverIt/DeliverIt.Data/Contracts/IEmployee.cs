namespace DeliverIt.Data.Models
{
    public interface IEmployee
    {
        int Id { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        int AddressId { get; }
        Address Address { get; }
    }
}