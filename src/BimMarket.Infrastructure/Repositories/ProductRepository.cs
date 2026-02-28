using BimMarket.Application.Admin.Products;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Application.Common.Models;
using BimMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;

    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<PagedResponse<ProductDto>> GetProductsAsync(int page, int pageSize, string? categoryId, string? search, CancellationToken ct = default)
    {
        var query = _db.Products.AsNoTracking()
            .Include(p => p.Images)
            .Where(p => p.DeletedAt == null);
        if (Guid.TryParse(categoryId, out var catId))
            query = query.Where(p => p.CategoryId == catId);
        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search) || (p.Sku != null && p.Sku.Contains(search)));
        var total = await query.CountAsync(ct);
        var items = await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        return new PagedResponse<ProductDto>
        {
            Items = items.Select(Map).ToList(),
            TotalCount = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var p = await _db.Products.AsNoTracking()
            .Include(x => x.Images)
            .FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null, ct);
        return p == null ? null : Map(p);
    }

    private static ProductDto Map(Domain.Entities.Product p) =>
        new(
            p.Id.ToString(),
            p.CategoryId.ToString(),
            p.Name,
            p.Slug,
            p.Description,
            p.Brand,
            (double)p.BasePrice,
            p.Sku,
            p.IsActive,
            p.IsFeatured,
            p.Images?.OrderBy(i => i.SortOrder).Select(i => new ProductImageDto(i.Id.ToString(), i.Url, i.ThumbnailUrl, i.SortOrder, i.IsMain)).ToList());
}
