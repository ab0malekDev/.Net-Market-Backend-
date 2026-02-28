using MediatR;

namespace BimMarket.Application.Admin.FlashDeals.Queries;

public record GetFlashDealsQuery : IRequest<List<FlashDealDto>>;
