using BimMarket.Application.Admin.Orders;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Application.Common.Models;
using BimMarket.Infrastructure.Data;
using BimMarket.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _db;

    public OrderRepository(AppDbContext db) => _db = db;

    public async Task<PagedResponse<OrderDto>> GetOrdersAsync(int page, int pageSize, string? status, CancellationToken ct = default)
    {
        var query = _db.Orders.AsNoTracking()
            .Include(o => o.Items).ThenInclude(i => i.Product)
            .Where(o => o.DeletedAt == null);
        if (!string.IsNullOrWhiteSpace(status))
            query = query.Where(o => o.Status == status);
        var total = await query.CountAsync(ct);
        var list = await query
            .OrderByDescending(o => o.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        var userIds = list.Select(o => o.UserId).Distinct().ToList();
        var users = await _db.Users.AsNoTracking().Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, ct);
        var branchIds = list.Select(o => o.BranchId).Distinct().ToList();
        var branches = await _db.Branches.AsNoTracking().Where(b => branchIds.Contains(b.Id))
            .ToDictionaryAsync(b => b.Id, ct);
        var items = list.Select(o => MapOrder(o, users.GetValueOrDefault(o.UserId), branches.GetValueOrDefault(o.BranchId))).ToList();
        return new PagedResponse<OrderDto> { Items = items, TotalCount = total, Page = page, PageSize = pageSize };
    }

    public async Task<OrderDetailDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var o = await _db.Orders.AsNoTracking()
            .Include(o => o.Items).ThenInclude(i => i.Product)
            .Include(o => o.StatusHistory)
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null, ct);
        if (o == null) return null;
        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == o.UserId, ct);
        var branch = await _db.Branches.AsNoTracking().FirstOrDefaultAsync(b => b.Id == o.BranchId, ct);
        return new OrderDetailDto(
            o.Id.ToString(),
            o.OrderNumber,
            o.Status,
            o.Items.Select(i => new OrderItemDto(i.Id.ToString(), i.ProductId.ToString(), i.Product?.Name, i.Quantity, (double)i.UnitPrice, (double)i.TotalPrice)).ToList(),
            (double)o.SubTotal,
            (double)o.DiscountAmount,
            (double)o.Total,
            o.PaymentMethod,
            o.CreatedAt.ToString("O"),
            o.StatusHistory?.OrderBy(h => h.CreatedAt).Select(h => new OrderStatusHistoryDto(h.Status, h.CreatedAt.ToString("O"), h.Notes)).ToList(),
            user != null ? $"{user.FirstName} {user.LastName}" : null,
            null);
    }

    public async Task<OrderDetailDto?> UpdateStatusAsync(Guid id, string status, string? notes, CancellationToken ct = default)
    {
        var o = await _db.Orders.Include(x => x.Items).ThenInclude(i => i.Product)
            .Include(x => x.StatusHistory)
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null, ct);
        if (o == null) return null;
        o.Status = status;
        o.UpdatedAt = DateTime.UtcNow;
        o.StatusHistory.Add(new Domain.Entities.OrderStatusHistory { Id = Guid.NewGuid(), OrderId = o.Id, Status = status, Notes = notes, CreatedAt = DateTime.UtcNow });
        await _db.SaveChangesAsync(ct);
        return await GetByIdAsync(id, ct);
    }

    private static OrderDto MapOrder(Domain.Entities.Order o, ApplicationUser? user, Domain.Entities.Branch? branch) =>
        new(
            o.Id.ToString(),
            o.OrderNumber,
            o.UserId.ToString(),
            o.BranchId.ToString(),
            o.Status,
            (double)o.SubTotal,
            (double)o.DiscountAmount,
            (double)o.Total,
            o.PaymentMethod,
            o.CreatedAt.ToString("O"),
            user != null ? $"{user.FirstName} {user.LastName}" : null,
            branch?.Name);
}
