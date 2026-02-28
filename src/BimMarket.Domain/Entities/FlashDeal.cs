namespace BimMarket.Domain.Entities;

public class FlashDeal
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public ICollection<FlashDealProduct> Products { get; set; } = new List<FlashDealProduct>();
}
