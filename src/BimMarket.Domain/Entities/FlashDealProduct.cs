namespace BimMarket.Domain.Entities;

public class FlashDealProduct
{
    public Guid Id { get; set; }
    public Guid FlashDealId { get; set; }
    public Guid ProductId { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public decimal? DiscountAmount { get; set; }
    public int? MaxQuantity { get; set; }
    public DateTime CreatedAt { get; set; }

    public FlashDeal FlashDeal { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
