namespace BimMarket.Domain.Entities;

public class Inventory
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid BranchId { get; set; }
    public int Quantity { get; set; }
    public int ReservedQuantity { get; set; }
    public int MinimumThreshold { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }

    public Product Product { get; set; } = null!;
    public Branch Branch { get; set; } = null!;
}
