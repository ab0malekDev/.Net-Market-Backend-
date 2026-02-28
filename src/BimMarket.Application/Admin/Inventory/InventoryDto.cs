namespace BimMarket.Application.Admin.Inventory;

public record InventoryDto(
    string Id,
    string ProductId,
    string BranchId,
    string? ProductName,
    string? BranchName,
    int Quantity,
    int ReservedQuantity,
    int MinimumThreshold,
    int AvailableQuantity);
