using BimMarket.Application.Admin.FlashDeals;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class FlashDealRepository : IFlashDealRepository
{
    private readonly AppDbContext _db;

    public FlashDealRepository(AppDbContext db) => _db = db;

    public async Task<List<FlashDealDto>> GetAllAsync(CancellationToken ct = default)
    {
        var list = await _db.FlashDeals.AsNoTracking()
            .Include(f => f.Products).ThenInclude(p => p.Product)
            .Where(f => f.DeletedAt == null)
            .ToListAsync(ct);
        return list.Select(f => new FlashDealDto(
            f.Id.ToString(),
            f.Title,
            f.StartAt.ToString("O"),
            f.EndAt.ToString("O"),
            f.IsActive,
            f.Products?.Select(p => new FlashDealProductDto(p.ProductId.ToString(), p.Product?.Name, (double?)p.DiscountPercentage, (double?)p.DiscountAmount)).ToList())).ToList();
    }
}
