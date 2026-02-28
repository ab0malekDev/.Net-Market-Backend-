using BimMarket.Application.Admin.Inventory;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class InventoryRepository : IInventoryRepository
{
    private readonly AppDbContext _db;

    public InventoryRepository(AppDbContext db) => _db = db;

    public async Task<List<InventoryDto>> GetAsync(string? branchId, string? productId, CancellationToken ct = default)
    {
        IQueryable<Domain.Entities.Inventory> query = _db.Inventory.AsNoTracking()
            .Include(x => x.Product)
            .Include(x => x.Branch);
        if (Guid.TryParse(branchId, out var bid))
            query = query.Where(x => x.BranchId == bid);
        if (Guid.TryParse(productId, out var pid))
            query = query.Where(x => x.ProductId == pid);
        var list = await query.ToListAsync(ct);
        return list.Select(Map).ToList();
    }

    public async Task<InventoryDto?> UpdateAsync(Guid id, int quantity, int minimumThreshold, CancellationToken ct = default)
    {
        var inv = await _db.Inventory.Include(x => x.Product).Include(x => x.Branch).FirstOrDefaultAsync(x => x.Id == id, ct);
        if (inv == null) return null;
        inv.Quantity = quantity;
        inv.MinimumThreshold = minimumThreshold;
        inv.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);
        return Map(inv);
    }

    private static InventoryDto Map(Domain.Entities.Inventory i) =>
        new(
            i.Id.ToString(),
            i.ProductId.ToString(),
            i.BranchId.ToString(),
            i.Product?.Name,
            i.Branch?.Name,
            i.Quantity,
            i.ReservedQuantity,
            i.MinimumThreshold,
            i.Quantity - i.ReservedQuantity);
}
