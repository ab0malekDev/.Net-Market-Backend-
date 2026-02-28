using BimMarket.Application.Admin.Coupons;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _db;

    public CouponRepository(AppDbContext db) => _db = db;

    public async Task<List<CouponDto>> GetAllAsync(CancellationToken ct = default)
    {
        var list = await _db.Coupons.AsNoTracking()
            .Where(c => c.DeletedAt == null)
            .ToListAsync(ct);
        return list.Select(c => new CouponDto(c.Id.ToString(), c.Code, c.Type, (double)c.Value, (double?)c.MinOrderAmount, c.MaxUsageCount, c.UsedCount, c.ValidFrom.ToString("O"), c.ValidTo.ToString("O"), c.IsActive)).ToList();
    }
}
