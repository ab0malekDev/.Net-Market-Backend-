using BimMarket.Application.Admin.Reviews;
using BimMarket.Application.Common.Abstractions;
using MediatR;

namespace BimMarket.Application.Admin.Reviews.Commands;

public class ApproveReviewCommandHandler(IReviewRepository repo) : IRequestHandler<ApproveReviewCommand, ReviewDto?>
{
    public Task<ReviewDto?> Handle(ApproveReviewCommand request, CancellationToken ct) =>
        repo.SetApprovedAsync(Guid.Parse(request.Id), request.IsApproved, ct);
}
