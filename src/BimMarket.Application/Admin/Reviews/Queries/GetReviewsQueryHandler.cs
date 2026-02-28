using BimMarket.Application.Admin.Reviews;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Reviews.Queries;

public class GetReviewsQueryHandler(IReviewRepository repo) : IRequestHandler<GetReviewsQuery, List<ReviewDto>>
{
    public Task<List<ReviewDto>> Handle(GetReviewsQuery request, CancellationToken ct) =>
        repo.GetAsync(request.Approved, ct);
}
