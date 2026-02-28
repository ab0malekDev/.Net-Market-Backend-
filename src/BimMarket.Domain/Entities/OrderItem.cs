namespace BimMarket.Domain.Entities;

public class OrderItem
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? FulfilledByBranchId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string? ProductSnapshot { get; set; }

    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
