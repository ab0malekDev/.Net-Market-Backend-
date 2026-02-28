using BimMarket.Application.Admin.Reviews;

namespace BimMarket.Application.Common.Abstractions;

public interface IReviewRepository
{
    Task<List<ReviewDto>> GetAsync(bool? approved, CancellationToken ct = default);
    Task<ReviewDto?> SetApprovedAsync(Guid id, bool isApproved, CancellationToken ct = default);
}
