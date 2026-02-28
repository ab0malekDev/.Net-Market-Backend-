namespace BimMarket.Domain.Entities;

public class Product
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }
    public string? Brand { get; set; }
    public decimal BasePrice { get; set; }
    public string Sku { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public bool IsFeatured { get; set; }
    public decimal? Weight { get; set; }
    public string Unit { get; set; } = "piece";
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Category Category { get; set; } = null!;
    public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
    public ICollection<Inventory> Inventory { get; set; } = new List<Inventory>();
}
