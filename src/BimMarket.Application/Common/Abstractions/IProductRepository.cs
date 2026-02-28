using BimMarket.Application.Common.Models;
using BimMarket.Application.Admin.Products;

namespace BimMarket.Application.Common.Abstractions;

public interface IProductRepository
{
    Task<PagedResponse<ProductDto>> GetProductsAsync(int page, int pageSize, string? categoryId, string? search, CancellationToken ct = default);
    Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
}
