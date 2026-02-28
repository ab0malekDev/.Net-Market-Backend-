using BimMarket.Application.Admin.Categories;

namespace BimMarket.Application.Common.Abstractions;

public interface ICategoryRepository
{
    Task<List<CategoryDto>> GetCategoriesAsync(bool includeChildren, CancellationToken ct = default);
}
