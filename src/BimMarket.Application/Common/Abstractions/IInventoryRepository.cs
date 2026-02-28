using BimMarket.Application.Admin.Inventory;

namespace BimMarket.Application.Common.Abstractions;

public interface IInventoryRepository
{
    Task<List<InventoryDto>> GetAsync(string? branchId, string? productId, CancellationToken ct = default);
    Task<InventoryDto?> UpdateAsync(Guid id, int quantity, int minimumThreshold, CancellationToken ct = default);
}
