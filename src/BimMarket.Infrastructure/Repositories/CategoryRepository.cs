using BimMarket.Application.Admin.Categories;
using BimMarket.Application.Common.Abstractions;
using BimMarket.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BimMarket.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _db;

    public CategoryRepository(AppDbContext db) => _db = db;

    public async Task<List<CategoryDto>> GetCategoriesAsync(bool includeChildren, CancellationToken ct = default)
    {
        var list = await _db.Categories.AsNoTracking()
            .Where(c => c.DeletedAt == null)
            .OrderBy(c => c.SortOrder)
            .ToListAsync(ct);
        var roots = list.Where(c => c.ParentId == null).ToList();
        if (!includeChildren)
            return roots.Select(c => new CategoryDto(c.Id.ToString(), null, c.Name, c.Slug, c.Description, c.ImageUrl, c.SortOrder, c.IsActive, null)).ToList();
        return roots.Select(c => MapWithChildren(c, list)).ToList();
    }

    private static CategoryDto MapWithChildren(Domain.Entities.Category c, List<Domain.Entities.Category> all)
    {
        var children = all.Where(x => x.ParentId == c.Id).OrderBy(x => x.SortOrder).Select(x => MapWithChildren(x, all)).ToList();
        return new CategoryDto(c.Id.ToString(), c.ParentId?.ToString(), c.Name, c.Slug, c.Description, c.ImageUrl, c.SortOrder, c.IsActive, children.Count > 0 ? children : null);
    }
}
