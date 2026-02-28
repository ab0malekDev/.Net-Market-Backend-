using BimMarket.Application.Admin.Coupons;

namespace BimMarket.Application.Common.Abstractions;

public interface ICouponRepository
{
    Task<List<CouponDto>> GetAllAsync(CancellationToken ct = default);
}
