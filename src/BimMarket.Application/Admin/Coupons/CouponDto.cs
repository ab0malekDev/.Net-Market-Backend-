namespace BimMarket.Application.Admin.Coupons;

public record CouponDto(
    string Id,
    string Code,
    string Type,
    double Value,
    double? MinOrderAmount,
    int? MaxUsageCount,
    int UsedCount,
    string ValidFrom,
    string ValidTo,
    bool IsActive);
