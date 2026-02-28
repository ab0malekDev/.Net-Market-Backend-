namespace BimMarket.Application.Admin.Branches;

public record BranchDto(
    string Id,
    string Name,
    string Address,
    string City,
    string? Region,
    string? Phone,
    bool IsActive,
    int SortOrder);
