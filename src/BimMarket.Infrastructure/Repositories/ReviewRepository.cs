using BimMarket.Application.Admin.Reviews;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Infrastructure.Data;
using BimMarket.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _db;

    public ReviewRepository(AppDbContext db) => _db = db;

    public async Task<List<ReviewDto>> GetAsync(bool? approved, CancellationToken ct = default)
    {
        var query = _db.Reviews.AsNoTracking()
            .Include(r => r.Product)
            .Where(r => r.DeletedAt == null);
        if (approved.HasValue)
            query = query.Where(r => r.IsApproved == approved.Value);
        var list = await query.OrderByDescending(r => r.CreatedAt).ToListAsync(ct);
        var userIds = list.Select(r => r.UserId).Distinct().ToList();
        var users = await _db.Users.AsNoTracking().Where(u => userIds.Contains(u.Id)).ToDictionaryAsync(u => u.Id, ct);
        return list.Select(r => Map(r, users.GetValueOrDefault(r.UserId))).ToList();
    }

    public async Task<ReviewDto?> SetApprovedAsync(Guid id, bool isApproved, CancellationToken ct = default)
    {
        var r = await _db.Reviews.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id, ct);
        if (r == null) return null;
        r.IsApproved = isApproved;
        r.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);
        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == r.UserId, ct);
        return Map(r, user);
    }

    private static ReviewDto Map(Domain.Entities.Review r, ApplicationUser? u) =>
        new(
            r.Id.ToString(),
            r.ProductId.ToString(),
            r.Product?.Name,
            r.UserId.ToString(),
            u != null ? $"{u.FirstName} {u.LastName}" : null,
            r.Rating,
            r.Title,
            r.Comment,
            r.IsApproved,
            r.CreatedAt.ToString("O"));
}
