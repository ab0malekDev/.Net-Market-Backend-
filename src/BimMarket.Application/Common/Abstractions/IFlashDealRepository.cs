using BimMarket.Application.Admin.FlashDeals;

namespace BimMarket.Application.Common.Abstractions;

public interface IFlashDealRepository
{
    Task<List<FlashDealDto>> GetAllAsync(CancellationToken ct = default);
}
