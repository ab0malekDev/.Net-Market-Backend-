using BimMarket.Application.Admin.Branches;

namespace BimMarket.Application.Common.Abstractions;

public interface IBranchRepository
{
    Task<List<BranchDto>> GetAllAsync(CancellationToken ct = default);

    Task<BranchDto> CreateAsync(
        string name,
        string address,
        string city,
        string? region,
        string? phone,
        bool isActive,
        int sortOrder,
        CancellationToken ct = default);

    Task<BranchDto?> UpdateAsync(
        Guid id,
        string name,
        string address,
        string city,
        string? region,
        string? phone,
        bool isActive,
        int sortOrder,
        CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
