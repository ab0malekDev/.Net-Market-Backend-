namespace BimMarket.Application.Admin.FlashDeals;

public record FlashDealProductDto(string ProductId, string? ProductName, double? DiscountPercentage, double? DiscountAmount);

public record FlashDealDto(
    string Id,
    string Title,
    string StartAt,
    string EndAt,
    bool IsActive,
    List<FlashDealProductDto>? Products = null);
