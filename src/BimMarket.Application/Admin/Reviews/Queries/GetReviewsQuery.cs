using MediatR;

namespace BimMarket.Application.Admin.Reviews.Queries;

public record GetReviewsQuery(bool? Approved = null) : IRequest<List<ReviewDto>>;
