namespace BimMarket.Domain.Entities;

public class Address
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Label { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public string? Region { get; set; }
    public string? PostalCode { get; set; }
    public string? Phone { get; set; }
    public bool IsDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
