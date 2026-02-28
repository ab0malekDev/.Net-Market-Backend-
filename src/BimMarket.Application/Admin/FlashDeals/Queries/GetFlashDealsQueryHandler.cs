using BimMarket.Application.Admin.FlashDeals;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.FlashDeals.Queries;

public class GetFlashDealsQueryHandler(IFlashDealRepository repo) : IRequestHandler<GetFlashDealsQuery, List<FlashDealDto>>
{
    public Task<List<FlashDealDto>> Handle(GetFlashDealsQuery request, CancellationToken ct) =>
        repo.GetAllAsync(ct);
}
