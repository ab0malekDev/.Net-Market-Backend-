using BimMarket.Application.Admin.Branches;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Domain.Entities;
using BimMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class BranchRepository : IBranchRepository
{
    private readonly AppDbContext _db;

    public BranchRepository(AppDbContext db) => _db = db;

    public async Task<List<BranchDto>> GetAllAsync(CancellationToken ct = default)
    {
        var list = await _db.Branches.AsNoTracking()
            .Where(b => b.DeletedAt == null)
            .OrderBy(b => b.SortOrder)
            .ToListAsync(ct);
        return list.Select(Map).ToList();
    }

    public async Task<BranchDto> CreateAsync(
        string name,
        string address,
        string city,
        string? region,
        string? phone,
        bool isActive,
        int sortOrder,
        CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var entity = new Branch
        {
            Id = Guid.NewGuid(),
            Name = name,
            Address = address,
            City = city,
            Region = region,
            Phone = phone,
            IsActive = isActive,
            SortOrder = sortOrder,
            CreatedAt = now,
            UpdatedAt = now
        };

        _db.Branches.Add(entity);
        await _db.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<BranchDto?> UpdateAsync(
        Guid id,
        string name,
        string address,
        string city,
        string? region,
        string? phone,
        bool isActive,
        int sortOrder,
        CancellationToken ct = default)
    {
        var entity = await _db.Branches.FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null, ct);
        if (entity == null) return null;

        entity.Name = name;
        entity.Address = address;
        entity.City = city;
        entity.Region = region;
        entity.Phone = phone;
        entity.IsActive = isActive;
        entity.SortOrder = sortOrder;
        entity.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _db.Branches.FirstOrDefaultAsync(b => b.Id == id && b.DeletedAt == null, ct);
        if (entity == null) return false;

        entity.DeletedAt = DateTime.UtcNow;
        entity.IsActive = false;
        await _db.SaveChangesAsync(ct);
        return true;
    }

    private static BranchDto Map(Branch b) =>
        new(
            b.Id.ToString(),
            b.Name,
            b.Address,
            b.City,
            b.Region,
            b.Phone,
            b.IsActive,
            b.SortOrder);
}
