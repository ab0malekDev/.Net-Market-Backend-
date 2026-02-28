using MediatR;

namespace BimMarket.Application.Admin.Reviews.Commands;

public record ApproveReviewCommand(string Id, bool IsApproved) : IRequest<ReviewDto?>;
